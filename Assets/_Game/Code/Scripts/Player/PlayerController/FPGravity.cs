using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPGravity : MonoBehaviour
{
    [SerializeField] private float groundGravity;
    [SerializeField] private float wallGravity;
    [SerializeField] private float airGravity;
    [NonSerialized] public float gravityForce;

    private Rigidbody _rigidbody;

    public void SetDefaultGravity()
    {
        gravityForce = groundGravity;
    }

    public void SetWallGravity()
    {
        gravityForce = wallGravity;
    }
    
    public void SetAirGravity()
    {
        gravityForce = airGravity;
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
        gravityForce = groundGravity;
    }
}