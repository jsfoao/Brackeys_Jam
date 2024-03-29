using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int highscore;
    int playerTile;
    int previousPlayerTile;
    
    private void Update()
    {
        playerTile = GameManager.Instance.spawnGenerator.PlayerTile;
        if (playerTile > previousPlayerTile)
        {
            score = playerTile;
        }
        if (score < 0)
        {
            score = 0;
        }
        previousPlayerTile = playerTile;
    }

    private void Awake()
    {
        previousPlayerTile = -2;
    }
}
