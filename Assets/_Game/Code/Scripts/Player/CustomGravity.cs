using System;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    [SerializeField] private float gravityMultiplier;
    
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Physics.gravity * gravityMultiplier);
    }
}
