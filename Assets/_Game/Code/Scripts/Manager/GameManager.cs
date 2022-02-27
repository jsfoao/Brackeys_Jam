using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [NonSerialized] public Entity controlledEntity;
    [NonSerialized] public RespawnManager respawnManager;
    
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
        SetResumeTimeScale();
    }

    public void SetPauseTimeScale()
    {
        Time.timeScale = 0f;
    }

    public void SetResumeTimeScale()
    {
        Time.timeScale = 1f;
    }
}
