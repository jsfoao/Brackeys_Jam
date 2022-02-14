using UnityEngine;

[RequireComponent(typeof(PlayerGrounding))]
public class PlayerJump : MonoBehaviour
{
    private PlayerGrounding _playerGrounding;
    private Rigidbody _rigidbody;
    
    [SerializeField] float jumpForce;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _playerGrounding.isGrounded)
        {
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnGUI()
    {
        Vector2 labelSize = new Vector2(200f, 200f);
        Vector2 labelPos = new Vector2(10f, 10f);
        Vector2 offset = new Vector2(0f, 15f);
        GUI.Label(new Rect(labelPos + (offset * 10), labelSize), $"<b>JUMPING</b>");
    }

    private void Start()
    {
        _playerGrounding = GetComponent<PlayerGrounding>();
        _rigidbody = GetComponent<Rigidbody>();
    }
}
