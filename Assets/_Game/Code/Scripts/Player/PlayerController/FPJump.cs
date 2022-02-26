using UnityEngine;

public class FPJump : MonoBehaviour
{
    [Header("Jump Force")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpForceWall;
    [Tooltip("How long to hold jump")]
    [SerializeField] private float jumpTime;
    [SerializeField] private Vector2 _wallJumpMultiplier;
    
    [Header("Buffers")]
    [SerializeField] private float groundCoyote;

    private float _currGroundCoyote;
    private float _jumpTimeCounter;
    private bool _isJumping;
    private bool _canGroundJump;

    private Rigidbody _rigidbody;
    private FPGrounding _fpGrounding;

    private bool _wallJump;
    private bool _groundJump;

    private void CoyoteTime()
    {
        // ground coyote
        if (_fpGrounding.isGrounded)
        {
            _currGroundCoyote = groundCoyote;
        }
        else
        {
            _currGroundCoyote -= Time.deltaTime;
        }
        _canGroundJump = _currGroundCoyote > 0;
    }
    
    private void Update()
    {
        CoyoteTime();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canGroundJump)
            {
                _groundJump = true;
                _isJumping = true;
                _jumpTimeCounter = jumpTime;
                _rigidbody.velocity += Vector3.up * _jumpForce * Time.fixedDeltaTime;
                
                _currGroundCoyote = 0f;
            }
            else if (_fpGrounding.isWalled)
            {
                _wallJump = true;
                _isJumping = true;
                _jumpTimeCounter = jumpTime;
                Vector3 direction = Vector3.up * _wallJumpMultiplier.x + _fpGrounding.wallNormal * _wallJumpMultiplier.y;
                if (direction.magnitude > 1)
                {
                    direction.Normalize();
                }
                _rigidbody.velocity += direction * _jumpForceWall * Time.fixedDeltaTime;
            }
        }
        
        if (Input.GetKey(KeyCode.Space) && _isJumping)
        {
            if (_groundJump)
            {
                if (_jumpTimeCounter > 0)
                {
                    _rigidbody.velocity += Vector3.up * _jumpForce * Time.deltaTime;
                }
                _jumpTimeCounter -= Time.deltaTime;
                _currGroundCoyote = 0f;
            }
            else if (_wallJump)
            {
                if (_jumpTimeCounter > 0)
                {
                    Vector3 direction = Vector3.up * _wallJumpMultiplier.x + _fpGrounding.wallNormal * _wallJumpMultiplier.y;
                    if (direction.magnitude > 1)
                    {
                        direction.Normalize();
                    }
                    _rigidbody.velocity += direction * _jumpForceWall * Time.deltaTime;
                }
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _groundJump = false;
                _wallJump = false;
                _isJumping = false;
                _jumpTimeCounter = jumpTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isJumping = false;
            _groundJump = false;
            _wallJump = false;
            _jumpTimeCounter = jumpTime;
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
        groundCoyote = Mathf.Max(0f, groundCoyote);
    }
}