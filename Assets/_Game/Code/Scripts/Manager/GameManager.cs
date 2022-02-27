using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [NonSerialized] public Entity controlledEntity;
    [NonSerialized] public SpawnGenerator spawnGenerator;
    [NonSerialized] public ScoreManager scoreManager;
    
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

        Time.timeScale = 1f;
        spawnGenerator = FindObjectOfType<SpawnGenerator>();
        scoreManager = GetComponent<ScoreManager>();
    }
}
