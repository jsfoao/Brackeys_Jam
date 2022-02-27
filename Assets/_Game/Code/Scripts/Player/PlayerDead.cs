using UnityEngine;
using UnityEngine.Events;

public class PlayerDead : MonoBehaviour
{
    [SerializeField] private float _deadHeight;
    public UnityEvent OnDeath;
    
    void Update()
    {
        if (transform.position.y < _deadHeight)
        {
            OnDeath.Invoke();
        }
    }
}
