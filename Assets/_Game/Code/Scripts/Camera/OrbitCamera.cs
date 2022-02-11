using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Tooltip("Camera distance radius")] private float radius;
    [SerializeField] private Vector2 orbitAngles;
    [SerializeField, Range(-89f, 89f)] private float minVerticalAngle;
    [SerializeField, Range(-89f, 89f)] private float maxVerticalAngle;
   
    
    private Quaternion _lookRotation;
    private bool isFacingCamera;
    
    public Vector2 Angles
    {
        get => orbitAngles;
        set => orbitAngles = value;
    }
    public float Radius
    {
        get => radius;
        set => radius = value;
    }
    public Transform Target
    {
        get => target;
    }
    
    private void ConstrainAngles()
    {
        orbitAngles.y = Mathf.Clamp(orbitAngles.y, minVerticalAngle, maxVerticalAngle);
    }
    
    private void LoopAngle()
    {
        if (orbitAngles.x < 0f)
        {
            orbitAngles.x += 360f;
        }
        else if (orbitAngles.x >= 360f)
        {
            orbitAngles.x -= 360f;
        }
    }

    private void ExecuteOrbit()
    {
        ConstrainAngles();
        LoopAngle();
        Quaternion newRotation = Quaternion.Euler(new Vector3(orbitAngles.y, orbitAngles.x, 0f));
        _lookRotation = newRotation;
        
        // looking at target
        Vector3 lookDirection = _lookRotation * Vector3.forward;
        Vector3 lookPosition = target.position - lookDirection * radius;
        
        // set pos and rotation
        transform.SetPositionAndRotation(lookPosition, _lookRotation);
    }
    
    private void LateUpdate()
    {
        if (target == null) { return; }
        Debug.DrawLine(target.position, target.transform.position, Color.green);
        Debug.DrawRay(target.position, target.forward, Color.blue);

        ExecuteOrbit();
    }

    private void OnValidate()
    {
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
        }
    }

    private void OnDrawGizmos()
    {
        if (target == null) { return; }
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(target.position, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(target.position, transform.position);
    }
}