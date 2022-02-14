using UnityEngine;

public class PlayerGrounding : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    [SerializeField] private LayerMask groundMask;
    [Header("RIDE")]
    [SerializeField] private float rideHeight;
    [SerializeField] private float rideSpringStrength;
    [SerializeField] private float rideSpringDamper;
    
    private float springForce;
    public bool isGrounded;

    private RaycastHit _hit;
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, rideHeight, groundMask))
        {
            isGrounded = true;
            Vector3 vel = _rigidbody.velocity;
            Vector3 rayDir = -transform.up;
    
            float rayDirVel = Vector3.Dot(rayDir, vel);
            
            float x = hit.distance - rideHeight;
            springForce = (x * rideSpringStrength) - (rayDirVel * rideSpringDamper);
            
            _rigidbody.AddForce(rayDir * springForce, ForceMode.Acceleration);
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * (rideHeight + 1));
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * rideHeight);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_hit.point, _hit.point + _hit.normal);
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_hit.point, .1f);
    }
}
