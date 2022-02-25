using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovetoSpawn : MonoBehaviour
{
    [SerializeField] private Transform playerSpawner;
    
    void Start()
    {
        transform.position = playerSpawner.transform.position;
    }
}
