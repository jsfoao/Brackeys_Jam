using System;
using UnityEngine;

public class FPGrounding : MonoBehaviour
{
    
    [Header("Grounding")]
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float rideRadius;
    [SerializeField] private float rideHeight;
    [SerializeField] private float rideSpringStrength;
    [SerializeField] private float rideSpringDamper;
    [NonSerialized] public Vector3 groundNormal;
    public bool isGrounded;
    private float springForce;
    private RaycastHit _groundHit;

    [Header("Walling")]
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private float wallcastOffset;
    [SerializeField] private float rayLengthRight;
    [SerializeField] private float rayLengthFront;
    [NonSerialized] public Vector3 wallNormal;
    public bool isWalled;
    private RaycastHit _wallHitLeft;
    private RaycastHit _wallHitRight;
    private RaycastHit _wallHitFront;
    private RaycastHit _wallHitBack;

    private Rigidbody _rigidbody;
    private FPLocomotion _fpLocomotion;

    private void WallCasting()
    {
        Vector3 offsetVec = transform.position + wallcastOffset * Vector3.down;
        if (Physics.Raycast(offsetVec, -_fpLocomotion.rightOrientation, out _wallHitLeft, rayLengthRight, wallMask))
        {
            isWalled = true;
            wallNormal = _wallHitLeft.normal;
        }
        else if (Physics.Raycast(offsetVec, _fpLocomotion.rightOrientation, out _wallHitRight, rayLengthRight, wallMask))
        {
            isWalled = true;
            wallNormal = _wallHitRight.normal;
        }
        else if (Physics.Raycast(offsetVec, -_fpLocomotion.forwardOrientation, out _wallHitBack, rayLengthFront, wallMask))
        {          
            isWalled = true;
            wallNormal = _wallHitBack.normal;
        }
        else if (Physics.Raycast(offsetVec, _fpLocomotion.forwardOrientation, out _wallHitFront, rayLengthFront, wallMask))
        {          
            isWalled = true;
            wallNormal = _wallHitFront.normal;
        }
        else
        {
            isWalled = false;
        }
    }
    private void GroundCasting()
    {
        if (Physics.SphereCast(transform.position, rideRadius, Vector3.down, out _groundHit, rideHeight, groundMask))
        {
            isGrounded = true;
            groundNormal = _groundHit.normal;

            Vector3 vel = _rigidbody.velocity;
            Vector3 rayDir = -transform.up;

            float rayDirVel = Vector3.Dot(rayDir, vel);
            
            float x = _groundHit.distance - rideHeight;
            springForce = (x * rideSpringStrength) - (rayDirVel * rideSpringDamper);
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Update()
    {
        GroundCasting();
        WallCasting();
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            _rigidbody.AddForce(-transform.up * springForce, ForceMode.Acceleration);
        }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _fpLocomotion = GetComponent<FPLocomotion>();
    }

    private void OnDrawGizmos()
    {
        #region Grounding

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, rideRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * rideHeight);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * rideHeight);

        if (isGrounded)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_groundHit.point, .1f);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_groundHit.point, _groundHit.point + _groundHit.normal);
        }

        #endregion

        _fpLocomotion = GetComponent<FPLocomotion>();
        #region Walling
        Gizmos.color = Color.red;
        Vector3 offsetVec = transform.position + wallcastOffset * Vector3.up;
        Gizmos.DrawSphere(offsetVec, .1f);
        Gizmos.DrawLine(offsetVec, offsetVec + (_fpLocomotion.rightOrientation * rayLengthRight));
        Gizmos.DrawLine(offsetVec, offsetVec + (-_fpLocomotion.rightOrientation * rayLengthRight));
        Gizmos.DrawLine(offsetVec, offsetVec + (-_fpLocomotion.forwardOrientation * rayLengthFront));
        Gizmos.DrawLine(offsetVec, offsetVec + (_fpLocomotion.forwardOrientation * rayLengthFront));
        #endregion
    }
}
