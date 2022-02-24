using UnityEngine;
using UnityEngine.Events;

public class FPWallLocomotion : MonoBehaviour
{
    public UnityEvent OnWallRide;

    private FPLocomotion _fpLocomotion;
    private FPGravity _fpGravity;
    private FPLook _fpLook;
    private FPGrounding _fpGrounding;

    private void Update()
    {
        if (!_fpGrounding.isGrounded && _fpGrounding.isWalled)
        {
            _fpGravity.SetWallGravity();
            float orientationDotWall = Vector3.Dot(_fpLocomotion.rightOrientation, _fpGrounding.wallNormal);
            if (orientationDotWall < 0)
            {
                _fpLook.tiltAngle = _fpLook.maxTiltAngle;
            }
            else
            {
                _fpLook.tiltAngle = -_fpLook.maxTiltAngle;
            }
        }
        else
        {
            _fpGravity.SetDefaultGravity();
            _fpLook.tiltAngle = 0f;
        }
    }

    private void Start()
    {
        _fpGrounding = GetComponent<FPGrounding>();
        _fpGravity = GetComponent<FPGravity>();
        _fpLook = GetComponent<FPLook>();
        _fpLocomotion = GetComponent<FPLocomotion>();
    }
}