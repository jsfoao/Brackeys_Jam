using System.Runtime.Remoting;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private AnimationCurve accelFactorFromDot;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deacceleration;
    
    private float accel;
    private Vector3 velocity;
    private Vector3 inputDirection;

    private Vector3 horizontalVelocity;
    private Vector3 moveDirection;
    private float velDotInput;
    public Vector3 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    private void FixedUpdate()
    {
        horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        
        // Projected camera direction
        Vector3 projectRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        Vector3 projectForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        
        // Calculating input vector
        Vector3 inputRight = projectRight * Input.GetAxisRaw("Horizontal");
        Vector3 inputForward = projectForward * Input.GetAxisRaw("Vertical");
        inputDirection = inputRight + inputForward;
        inputDirection.y = 0;
        
        if (inputDirection.magnitude > 1f)
        {
            inputDirection.Normalize();
        }
        
        accel = inputDirection.magnitude <= 0 ? deacceleration : acceleration;
        velDotInput = Vector3.Dot(inputDirection, horizontalVelocity.normalized);

        float accelFinal = accel * accelFactorFromDot.Evaluate(velDotInput);
        moveDirection = Vector3.MoveTowards(moveDirection, inputDirection, accelFinal * Time.fixedDeltaTime);
        _rigidbody.velocity = moveDirection * maxSpeed;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnGUI()
    {
        Vector2 labelSize = new Vector2(200f, 200f);
        Vector2 labelPos = new Vector2(10f, 10f);
        Vector2 offset = new Vector2(0f, 15f);
        GUI.Label(new Rect(labelPos + (offset * 1), labelSize), $"<b>LOCOMOTION</b>");
        GUI.Label(new Rect(labelPos + (offset * 2), labelSize), $"Input: {inputDirection}");
        GUI.Label(new Rect(labelPos + (offset * 3), labelSize), $"Velocity: {_rigidbody.velocity}");
        GUI.Label(new Rect(labelPos + (offset * 4), labelSize), $"Speed: {_rigidbody.velocity.magnitude}");
        GUI.Label(new Rect(labelPos + (offset * 5), labelSize), $"Vel Dot: {velDotInput}");
        GUI.Label(new Rect(labelPos + (offset * 6), labelSize), $"Accel Factor: {accelFactorFromDot.Evaluate(velDotInput)}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 1f);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + horizontalVelocity.normalized);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + inputDirection, .05f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + moveDirection);
    }
}
