using System;
using System.ComponentModel.Design;
using UnityEngine;

public class FPGrounding : MonoBehaviour
{
    
    [Header("Grounding")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float rideHeight;
    [SerializeField] private float rideSpringStrength;
    [SerializeField] private float rideSpringDamper;
    [NonSerialized] public Vector3 groundNormal;
    [NonSerialized] public bool isGrounded;
    private float springForce;
    private RaycastHit _groundHit;

    [Header("Walling")]
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private float rayLengthRight;
    [SerializeField] private float rayLengthFront;
    [NonSerialized] public Vector3 wallNormal;
    [NonSerialized] public bool isWalled;
    private RaycastHit _wallHitLeft;
    private RaycastHit _wallHitRight;
    private RaycastHit _wallHitFront;
    private RaycastHit _wallHitBack;
    public RaycastHit closestWallHit;

    private Rigidbody _rigidbody;
    private FPLocomotion _fpLocomotion;

    private void WallCasting()
    {
        int flag = 0;
        if (Physics.Raycast(transform.position, -_fpLocomotion.rightOrientation, out _wallHitLeft, rayLengthRight, wallMask))
        {
            isWalled = true;
            wallNormal = _wallHitLeft.normal;
        }
        else if (Physics.Raycast(transform.position, _fpLocomotion.rightOrientation, out _wallHitRight, rayLengthRight, wallMask))
        {
            isWalled = true;
            wallNormal = _wallHitRight.normal;
        }
        else if (Physics.Raycast(transform.position, -_fpLocomotion.forwardOrientation, out _wallHitBack, rayLengthFront, wallMask))
        {          
            isWalled = true;
            wallNormal = _wallHitBack.normal;
        }
        else if (Physics.Raycast(transform.position, _fpLocomotion.forwardOrientation, out _wallHitFront, rayLengthFront, wallMask))
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
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, rideHeight, groundMask))
        {
            isGrounded = true;
            groundNormal = hit.normal;

            Vector3 vel = _rigidbody.velocity;
            Vector3 rayDir = -transform.up;

            float rayDirVel = Vector3.Dot(rayDir, vel);
            
            float x = hit.distance - rideHeight;
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
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * (rideHeight + 1));
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * rideHeight);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_groundHit.point, _groundHit.point + _groundHit.normal);
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_groundHit.point, .1f);
        #endregion

        _fpLocomotion = GetComponent<FPLocomotion>();
        #region Walling
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (_fpLocomotion.rightOrientation * rayLengthRight));
        Gizmos.DrawLine(transform.position, transform.position + (-_fpLocomotion.rightOrientation * rayLengthRight));
        Gizmos.DrawLine(transform.position, transform.position + (-_fpLocomotion.forwardOrientation * rayLengthFront));
        Gizmos.DrawLine(transform.position, transform.position + (_fpLocomotion.forwardOrientation * rayLengthFront));
        if (isWalled)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(closestWallHit.point, .1f);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(closestWallHit.point, wallNormal);
        }
        #endregion
    }
}
