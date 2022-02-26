using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    [SerializeField] private float _deadHeight;
    
    void Update()
    {
        if (transform.position.y < _deadHeight)
        {
            GameManager.Instance.respawnManager.Respawn();
        }
    }
}
