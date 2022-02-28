using System;
using UnityEngine;

public class FPLocomotion : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [NonSerialized] public Vector3 rightOrientation;
    [NonSerialized] public Vector3 forwardOrientation;
    
    [Header("Ground")]
    [SerializeField] private AnimationCurve accelFactorGround;
    [SerializeField] private float groundSpeed;
    [SerializeField] private float groundAcceleration;
    [SerializeField] private float groundDeacceleration;
    [SerializeField, Range(0, 1)] private float sideSpeedMultiplier;
    [SerializeField, Range(0, 1)] private float backSpeedMultiplier;
    
    
    [Header("Air")]
    [SerializeField] private AnimationCurve accelFactorAir;
    [SerializeField] private float airSpeed;
    [SerializeField] private float airAcceleration;
    [SerializeField] private float airDeacceleration;
    
    private Vector3 velocity;
    private Vector3 inputDirection;

    private Vector3 horizontalVelocity;
    private Vector3 moveDirection;
    private float velDotInput;

    private Rigidbody _rigidbody;
    private FPGrounding _fpGrounding;
    private FPGravity _fpGravity;

    private void Update()
    {
        if (!_fpGrounding.isGrounded && !_fpGrounding.isWalled)
        {
            _fpGravity.SetAirGravity();
        }
        else if (_fpGrounding.isGrounded)
        {
            _fpGravity.SetDefaultGravity();
        }
    }

    private void FixedUpdate()
    {
        horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        
        // Projected camera direction
        rightOrientation = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        forwardOrientation = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        
        // Calculating input vector
        Vector3 inputRight = rightOrientation * Input.GetAxisRaw("Horizontal");
        Vector3 inputForward = forwardOrientation * Input.GetAxisRaw("Vertical");
        inputDirection = inputRight + inputForward;
        inputDirection.y = 0;
        
        if (inputDirection.magnitude > 1f)
        {
            inputDirection.Normalize();
        }

        velDotInput = Vector3.Dot(inputDirection, horizontalVelocity.normalized);
        float orientationDotInput = Vector3.Dot(inputDirection, forwardOrientation);

        float speedMultiplier = 1;
        // TODO different speed depending on movement direction
        // if (orientationDotInput < -0.2f)
        // {
        //     speedMultiplier = backSpeedMultiplier;
        // }
        // else if (orientationDotInput > 0.2f)
        // {
        //     speedMultiplier = orientationDotInput;
        // }
        // else
        // {
        //     speedMultiplier = sideSpeedMultiplier;
        // }

        inputDirection = Vector3.ProjectOnPlane(inputDirection, _fpGrounding.groundNormal);
        if (_fpGrounding.isGrounded)
        {
            float accel = inputDirection.magnitude <= 0 ? groundDeacceleration : groundAcceleration;
            float accelFinal = accel * accelFactorGround.Evaluate(velDotInput);
            moveDirection = Vector3.MoveTowards(moveDirection, inputDirection, accelFinal * Time.fixedDeltaTime);
            _rigidbody.velocity += moveDirection * groundSpeed * speedMultiplier;
        }
        else
        {
            float accel = inputDirection.magnitude <= 0 ? airDeacceleration : airAcceleration;
            float accelFinal = accel * accelFactorAir.Evaluate(velDotInput);
            moveDirection = Vector3.MoveTowards(moveDirection, inputDirection, accelFinal * Time.fixedDeltaTime);
            _rigidbody.velocity += moveDirection * airSpeed * speedMultiplier;
        }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _fpGrounding = GetComponent<FPGrounding>();
        _fpGravity = GetComponent<FPGravity>();
    }

    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.white;
        // Gizmos.DrawWireSphere(transform.position, 1f);
        //
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawLine(transform.position, transform.position + horizontalVelocity.normalized);
        //
        // Gizmos.color = Color.red;
        // Gizmos.DrawSphere(transform.position + inputDirection, .05f);
        //
        // Gizmos.color = Color.red;
        // Gizmos.DrawLine(transform.position, transform.position + moveDirection);
    }
}
