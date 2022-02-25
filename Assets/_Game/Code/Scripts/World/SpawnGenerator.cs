using System;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tileSpawn;
    [SerializeField] private Transform player;
    
    public int mapLength;
    public int mapWidth;
    private float tileDistanceX;
    private float tileDistanceZ;
    [SerializeField] private Transform chunk;
    
    [NonSerialized] public int playerTile = 0;

    private int mapForwardEdge;
    private int realTile;
    private int rowReals;
    private GameObject tileSpawner;

    public float realProb = 0.5f;

    void Start()
    {
        mapForwardEdge = mapLength-1;
        tileDistanceX = chunk.localScale.x;
        tileDistanceZ = chunk.localScale.z;

        for (int j = 0; j < mapLength; j++)
        {
            rowReals = 0;
            for (int i=0; i < mapWidth; i++)
            {
                if (i == mapWidth-1 && rowReals == 0)
                {
                    tileSpawner = Instantiate(tileSpawn, new Vector3(tileDistanceX*i, 0, tileDistanceZ*j), Quaternion.identity);
                    tileSpawner.GetComponent<TileSpawner>().row = j;
                    tileSpawner.GetComponent<TileSpawner>().isReal = true;
                }
                else
                {
                    tileSpawner = Instantiate(tileSpawn, new Vector3(tileDistanceX*i, 0, tileDistanceZ*j), Quaternion.identity);
                    tileSpawner.GetComponent<TileSpawner>().row = j;
                    if (Random.value > 0.5f)
                    {
                        tileSpawner.GetComponent<TileSpawner>().isReal = true;
                        rowReals += 1;
                    }
                }
            }
        }
    }
    
    void Update()
    {
        playerTile = Mathf.FloorToInt((player.position.z + (tileDistanceZ / 2)) / tileDistanceZ);
        if ((playerTile + mapLength) > mapForwardEdge)
        {
            mapForwardEdge += 1;
            rowReals = 0;
            for (int i=0; i < mapWidth; i++)
            {
                if (i == mapWidth - 1 && rowReals == 0)
                {
                    tileSpawner = Instantiate(tileSpawn, new Vector3(tileDistanceX*i, 0, mapForwardEdge*tileDistanceZ), Quaternion.identity);
                    tileSpawner.GetComponent<TileSpawner>().row = mapForwardEdge;
                    tileSpawner.GetComponent<TileSpawner>().isReal = true;
                }
                else
                {
                    tileSpawner = Instantiate(tileSpawn, new Vector3(tileDistanceX*i, 0, mapForwardEdge*tileDistanceZ), Quaternion.identity);
                    tileSpawner.GetComponent<TileSpawner>().row = mapForwardEdge;
                    if (Random.value <= realProb)
                    {
                        tileSpawner.GetComponent<TileSpawner>().isReal = true;
                        rowReals += 1;
                    }
                }
                
            }
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10f, 10f, 200f, 200f), playerTile.ToString());
    }
}
