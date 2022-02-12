using UnityEngine;

public class PlayerGrounding : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float offset;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance;
    [SerializeField] private bool isGrounded;
    private float _rayDistance;
    private Vector3 _origin;

    private void Update()
    {
        RaycastHit hit;
        _origin = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
        if (Physics.Raycast(_origin, Vector3.down, out hit, _rayDistance, groundMask))
        {
            float distance = (transform.position - hit.point).magnitude;
            isGrounded = distance <= offset + groundDistance;
            Debug.Log(distance);
        }

        if (isGrounded)
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + groundDistance + offset, transform.position.z);
        }
        else
        {
            transform.position += new Vector3(0f, -1f, 0f) * gravity * Time.deltaTime;
        }
    }

    private void OnValidate()
    {
        _rayDistance = offset + groundDistance + 1;
    }

    private void OnDrawGizmos()
    {
        _origin = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(_origin, transform.position + Vector3.down * (offset + groundDistance));
    }
}
