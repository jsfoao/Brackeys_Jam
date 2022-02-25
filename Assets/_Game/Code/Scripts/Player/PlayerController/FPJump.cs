using UnityEngine;

public class FPJump : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpForceWall;
    [Tooltip("How long to hold jump")]
    [SerializeField] private float jumpTime;
    [SerializeField] private Vector2 _wallJumpMultiplier;

    float jumpTimeCounter;
    bool isJumping;

    private Rigidbody _rigidbody;
    private FPGrounding _fpGrounding;

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
                _rigidbody.velocity += Vector3.up * _jumpForce * Time.fixedDeltaTime;
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
                _rigidbody.velocity += direction * _jumpForceWall * Time.fixedDeltaTime;
            }
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (_groundJump)
            {
                if (jumpTimeCounter > 0)
                {
                    _rigidbody.velocity += Vector3.up * _jumpForce * Time.deltaTime;
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
                    _rigidbody.velocity += direction * _jumpForceWall * Time.deltaTime;
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
    }

    private void OnValidate()
    {
        _wallJumpMultiplier.x = Mathf.Clamp01(_wallJumpMultiplier.x);
        _wallJumpMultiplier.y = Mathf.Clamp01(_wallJumpMultiplier.y);
    }
}