using System;
using UnityEngine;

public class CurveTest : MonoBehaviour
{
    [SerializeField] private AnimationCurve _acceleration;
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;
    [SerializeField] private float multiplier;
    
    private float _time;
    private void Update()
    {
        _time += Time.deltaTime;
        transform.position = Vector3.Lerp(pointA, pointB, _acceleration.Evaluate(Time.time * multiplier));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pointA, .2f);
        Gizmos.DrawSphere(pointB, .2f);
        
        Gizmos.color = Color.white;
        Gizmos.DrawLine(pointA, pointB);
    }
}
