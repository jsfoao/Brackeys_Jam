using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deacceleration;
    private float accel;
    private float velocity;
    
    private Vector3 inputDirection;
    private Vector3 moveDirection;
    
    private void Update()
    {
        // Projected camera direction
        Vector3 projectRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        Vector3 projectForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        
        // Calculating input vector
        Vector3 inputRight = projectRight * Input.GetAxisRaw("Horizontal");
        Vector3 inputForward = projectForward * Input.GetAxisRaw("Vertical");
        inputDirection = inputRight + inputForward;
        inputDirection.y = 0;
        
        // Calculating move direction
        accel = inputDirection.magnitude <= 0 ? deacceleration : acceleration;
        moveDirection = Vector3.MoveTowards(moveDirection, inputDirection, accel * Time.deltaTime);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
        velocity = moveDirection.magnitude * maxSpeed;
        
        transform.position += moveDirection * maxSpeed * Time.deltaTime;
    }

    private void OnGUI()
    {
        Vector2 labelSize = new Vector2(200f, 200f);
        Vector2 labelPos = new Vector2(10f, 10f);
        Vector2 offset = new Vector2(0f, 15f);
        GUI.Label(new Rect(labelPos, labelSize), $"Input: {inputDirection}");
        GUI.Label(new Rect(labelPos + offset, labelSize), $"Move Direction: {moveDirection}");
        GUI.Label(new Rect(labelPos + (offset * 2), labelSize), $"Velocity: {velocity}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 1f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(moveDirection.x, 0f, 0f));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0f, 0f, moveDirection.z));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(moveDirection.x, 0f, moveDirection.z));

    }
}
