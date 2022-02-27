using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Entity _entityRef;
    [SerializeField] private float range;
    [SerializeField] private float awarenessGain;
    

    private void Update()
    {
        float distance = (transform.position - _entityRef.transform.position).magnitude;
        if (distance < range)
        {
            _entityRef.awareness += awarenessGain;
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _entityRef = GameManager.Instance.controlledEntity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
