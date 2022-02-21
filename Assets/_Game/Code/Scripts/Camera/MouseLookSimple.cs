using Unity.Mathematics;
using UnityEngine;

public class MouseLookSimple : MonoBehaviour
{
    public float MouseSensitivity;
    private float verticalRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f);
        
        transform.parent.Rotate(Vector3.up * mouseX);
        //PlayerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}