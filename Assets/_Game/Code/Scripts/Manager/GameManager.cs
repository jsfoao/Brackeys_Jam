using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [NonSerialized] public Entity controlledEntity;
    public RespawnManager respawnManager;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        respawnManager = GetComponent<RespawnManager>();
    }
}
