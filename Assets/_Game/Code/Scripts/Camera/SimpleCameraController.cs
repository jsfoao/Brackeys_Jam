using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    [SerializeField] private float mouseSpeed;
    [SerializeField] private Vector2 angle;
    
    private void Update()
    {
        angle.x += Input.GetAxisRaw("Mouse X") * mouseSpeed;
        angle.y += Input.GetAxisRaw("Mouse Y") * mouseSpeed;
        transform.rotation = Quaternion.Euler(angle.y, angle.x, 0f);
    }
}
