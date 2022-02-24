using System;
using UnityEngine;

public class FPGrounding : MonoBehaviour
{
    #region Floater
    private Rigidbody _rigidbody;
    
    [Header("Grounding")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float rideHeight;
    [SerializeField] private float rideSpringStrength;
    [SerializeField] private float rideSpringDamper;
    [NonSerialized] public Vector3 groundNormal;
    public bool isGrounded;
    private float springForce;
    private RaycastHit _groundHit;

    [Header("Walling")]
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private float rayLength;
    [NonSerialized] public Vector3 wallNormal;
    public bool isWalled;
    private RaycastHit _wallHitLeft;
    private RaycastHit _wallHitRight;
    public RaycastHit closestWallHit;


    private FPLocomotion _fpLocomotion;

    private void WallCasting()
    {
        if (Physics.Raycast(transform.position, -_fpLocomotion.rightOrientation, out _wallHitLeft, rayLength, wallMask))
        {
            isWalled = true;
            closestWallHit = _wallHitLeft;
            wallNormal = closestWallHit.normal;
        }
        else if (Physics.Raycast(transform.position, _fpLocomotion.rightOrientation, out _wallHitRight, rayLength, wallMask))
        {
            isWalled = true;
            closestWallHit = _wallHitRight;
            wallNormal = closestWallHit.normal;
        }
        else if (Physics.Raycast(transform.position, _fpLocomotion.forwardOrientation, out _wallHitRight, rayLength, wallMask))
        {
            
        }
        else if (Physics.Raycast(transform.position, _fpLocomotion.forwardOrientation, out _wallHitRight, rayLength, wallMask))
        {
            
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
        Gizmos.DrawLine(transform.position, transform.position + (_fpLocomotion.rightOrientation * rayLength));
        Gizmos.DrawLine(transform.position, transform.position + (-_fpLocomotion.rightOrientation * rayLength));
        if (isWalled)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(closestWallHit.point, .1f);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(closestWallHit.point, wallNormal);
        }
        #endregion
    }
    #endregion
}
