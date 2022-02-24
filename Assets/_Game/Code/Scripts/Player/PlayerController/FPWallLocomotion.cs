using UnityEngine;

public class FPWallLocomotion : MonoBehaviour
{
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
            if (orientationDotWall < -0.2f)
            {
                _fpLook.tiltAngle = _fpLook.maxTiltAngle;
            }
            else if (orientationDotWall > 0.2f)
            {
                _fpLook.tiltAngle = -_fpLook.maxTiltAngle;
            }
            else
            {
                _fpLook.tiltAngle = 0f;
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