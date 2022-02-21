using Unity.Mathematics;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform PlayerBody;
    
    public float MouseSensitivity;
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f);
        horizontalRotation += mouseX;
        horizontalRotation = Mathf.Clamp(horizontalRotation, -80f, 80f);

        if (horizontalRotation >= 80f || horizontalRotation <= -80f)
        {
            PlayerBody.Rotate(Vector3.up * mouseX);
        }
        
        transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }
}
