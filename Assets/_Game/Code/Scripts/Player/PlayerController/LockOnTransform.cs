using UnityEngine;

public class LockOnTransform : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Transform _targetTransform;
    
    private void Update()
    {
        transform.position = _targetTransform.position;
    }

    private void Start()
    {
        _targetTransform = target.transform;
    }
}
