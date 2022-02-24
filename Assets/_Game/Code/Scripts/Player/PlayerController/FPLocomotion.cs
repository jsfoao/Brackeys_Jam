using System;
using UnityEngine;

public class FPLocomotion : MonoBehaviour
{
    [Header("Keybinds")]
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    
    [SerializeField] private Transform cameraOrientation;
    [NonSerialized] public Vector3 forwardOrientation;
    [NonSerialized] public Vector3 rightOrientation;

    [Header("Movement")]

    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] private float groundMultiplier = 10f;
    [SerializeField] private float airMultiplier = 0.4f;
    

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;
    
    private float _moveSpeed = 6f;
    Vector3 _moveDirection;
    private Vector3 _inputDirection;


    RaycastHit slopeHit;

    private const string horizontalAxis = "Horizontal";
    private const string verticalAxis = "Vertical";
    
    private Rigidbody _rigidbody;
    private FPGrounding _fpGrounding;

    void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && _fpGrounding.isGrounded)
        {
            _moveSpeed = Mathf.Lerp(_moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            _moveSpeed = Mathf.Lerp(_moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        if (_fpGrounding.isGrounded)
        {
            _rigidbody.drag = groundDrag;
        }
        else
        {
            _rigidbody.drag = airDrag;
        }
    }

    void MovePlayer()
    {
        if (_fpGrounding.isGrounded)
        {
            _rigidbody.AddForce(_moveDirection.normalized * _moveSpeed * groundMultiplier, ForceMode.Acceleration);
        }
        else if (!_fpGrounding.isGrounded)
        {
            _rigidbody.AddForce(_moveDirection.normalized * _moveSpeed * groundMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    private void Update()
    {
        ControlDrag();
        ControlSpeed();

        forwardOrientation = Vector3.ProjectOnPlane(cameraOrientation.forward, Vector3.up).normalized;
        rightOrientation = Vector3.ProjectOnPlane(cameraOrientation.right, Vector3.up).normalized;
        _inputDirection = forwardOrientation * Input.GetAxisRaw(verticalAxis) + rightOrientation * Input.GetAxisRaw(horizontalAxis);
        _moveDirection = Vector3.ProjectOnPlane(_inputDirection, _fpGrounding.groundNormal);
        
        if (_moveDirection.magnitude > 1)
        {
            _moveDirection.Normalize();
        }
    }
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _fpGrounding = GetComponent<FPGrounding>();
        
        _rigidbody.freezeRotation = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 1f);
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _moveDirection);

        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, Vector3.ProjectOnPlane(_inputDirection, Vector3.up));

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, forwardOrientation);
        Gizmos.DrawRay(transform.position, rightOrientation);
    }
}