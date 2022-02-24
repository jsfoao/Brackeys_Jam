using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPGravity : MonoBehaviour
{
    [SerializeField] private float defaultGravity;
    [SerializeField] private float wallGravity;
    public float gravityForce;

    private Rigidbody _rigidbody;

    public void SetDefaultGravity()
    {
        gravityForce = defaultGravity;
    }

    public void SetWallGravity()
    {
        gravityForce = wallGravity;
    }

    public void SetGravity(float force)
    {
        gravityForce = force;
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(Vector3.up * gravityForce);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        gravityForce = defaultGravity;
    }
}