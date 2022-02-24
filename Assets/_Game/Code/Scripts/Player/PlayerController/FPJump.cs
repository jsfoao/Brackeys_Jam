using UnityEditor.ShaderGraph;
using UnityEngine;

public class FPJump : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpForceWall;
    [SerializeField] private Vector2 _wallJumpMultiplier;
    [SerializeField] private float jumpTime;

    float jumpTimeCounter;
    bool isJumping;
    bool jumpKeyHeld;

    private Rigidbody _rigidbody;
    private FPGrounding _fpGrounding;
    private FPGravity _fpGravity;

    private bool _wallJump;
    private bool _groundJump;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_fpGrounding.isGrounded)
            {
                _groundJump = true;
                isJumping = true;
                jumpTimeCounter = jumpTime;
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Force);   
            }
            else if (_fpGrounding.isWalled)
            {
                _wallJump = true;
                isJumping = true;
                jumpTimeCounter = jumpTime;
                Vector3 direction = Vector3.up * _wallJumpMultiplier.x + _fpGrounding.wallNormal * _wallJumpMultiplier.y;
                if (direction.magnitude > 1)
                {
                    direction.Normalize();
                }
                _rigidbody.AddForce(direction * _jumpForceWall, ForceMode.Force);   
            }
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (_groundJump)
            {
                if (jumpTimeCounter > 0)
                {
                    _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Force);
                }
                jumpTimeCounter -= Time.deltaTime;
            }
            else if (_wallJump)
            {
                if (jumpTimeCounter > 0)
                {
                    Vector3 direction = Vector3.up * _wallJumpMultiplier.x + _fpGrounding.wallNormal * _wallJumpMultiplier.y;
                    if (direction.magnitude > 1)
                    {
                        direction.Normalize();
                    }
                    _rigidbody.AddForce(direction * _jumpForceWall, ForceMode.Force);   
                }
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _groundJump = false;
                _wallJump = false;
                isJumping = false;
                jumpTimeCounter = jumpTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            _groundJump = false;
            _wallJump = false;
            jumpTimeCounter = jumpTime;
        }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _fpGrounding = GetComponent<FPGrounding>();
        _fpGravity = GetComponent<FPGravity>();
    }

    private void OnValidate()
    {
        _wallJumpMultiplier.x = Mathf.Clamp01(_wallJumpMultiplier.x);
        _wallJumpMultiplier.y = Mathf.Clamp01(_wallJumpMultiplier.y);
    }
}