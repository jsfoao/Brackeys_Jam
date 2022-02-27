using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnGenerator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform spawnStruct;
    
    
    public List<TilePrefab> prefabs;
    private List<GameObject> list;
    public GameObject wallPrefab;
    
    public int mapLength;
    public int mapWidth;
    public float tileSizeX;
    public float tileSizeZ;

    [NonSerialized] public int PlayerTile = 0;

    private int _mapForwardEdge;
    private int _rowReals;

    public float realProb = 0.5f;
    public float wallProb = 0.5f;

    void Start()
    {
        list = new List<GameObject>();
        foreach (var t in prefabs)
        {
            int i = 0;
            while (i < t.weight)
            {
                list.Add(t.prefab);
                i += 1;
            }
        }
        foreach (var t in list)
        {
            t.transform.localScale = new Vector3(tileSizeX, t.transform.localScale.y, tileSizeZ);
        }

        var localScale = wallPrefab.transform.localScale;
        localScale = new Vector3(localScale.x, localScale.y, tileSizeZ);
        wallPrefab.transform.localScale = localScale;

        spawnStruct.localScale = new Vector3(tileSizeX * mapWidth, spawnStruct.localScale.y, spawnStruct.localScale.z);
        player.position = spawnStruct.GetChild(2).position;

        _mapForwardEdge = mapLength;
        
        for (int j = 0; j < mapLength; j++)
        {
            _rowReals = 0;
            
            // Tile Generation
            for (int i=0; i < mapWidth; i++)
            {
                GameObject tile = Instantiate(list[Random.Range(0, list.Count)], new Vector3(tileSizeX*i, 0, (tileSizeZ/2)+tileSizeZ*j), Quaternion.identity);
                tile.GetComponent<ChunkInfo>().row = j+1;
                if (i == mapWidth-1 && _rowReals == 0)
                {
                    tile.GetComponent<ChunkInfo>().isReal = true;
                }
                else
                {
                    if (Random.value <= realProb)
                    {
                        tile.GetComponent<ChunkInfo>().isReal = true;
                        _rowReals += 1;
                    }
                }
            }

            // Wall Generation
            for (int i = 0; i < mapWidth + 1; i++)
            {
                if (Random.value <= wallProb)
                {
                    GameObject wall = Instantiate(wallPrefab, new Vector3((-tileSizeX/2)+(tileSizeX * i), 0, (tileSizeZ/2)+tileSizeZ * j),
                        Quaternion.identity);
                    wall.GetComponent<ChunkInfo>().row = j+1;
                    if (Random.value <= realProb)
                    {
                        wall.GetComponent<ChunkInfo>().isReal = true;
                    }
                }
            }
        }
    }
    
    void Update()
    {
        PlayerTile = Mathf.CeilToInt(player.position.z / tileSizeZ);
        if ((PlayerTile + mapLength) > _mapForwardEdge)
        {
            _mapForwardEdge += 1;
            _rowReals = 0;
            
            // Tile Generation
            for (int i=0; i < mapWidth; i++)
            {
                int rand = Random.Range(0, list.Count);
                GameObject tile = Instantiate(list[rand], new Vector3(tileSizeX*i, 0, (tileSizeZ/2)+tileSizeZ*(_mapForwardEdge-1)), Quaternion.identity);
                tile.GetComponent<ChunkInfo>().row = _mapForwardEdge;

                if (i == mapWidth-1 && _rowReals == 0)
                {
                    tile.GetComponent<ChunkInfo>().isReal = true;
                }
                else
                {
                    if (Random.value <= realProb)
                    {
                        tile.GetComponent<ChunkInfo>().isReal = true;
                        _rowReals += 1;
                    }
                }
            }
            
            // Wall generation
            for (int i = 0; i < mapWidth + 1; i++)
            {
                if (Random.value <= wallProb)
                {
                    GameObject wall = Instantiate(wallPrefab, new Vector3((-tileSizeX/2)+(tileSizeX * i), 0, (tileSizeZ/2)+tileSizeZ * (_mapForwardEdge-1)),
                        Quaternion.identity);
                    wall.GetComponent<ChunkInfo>().row = _mapForwardEdge;
                    if (Random.value <= realProb)
                    {
                        wall.GetComponent<ChunkInfo>().isReal = true;
                    }
                }
            }
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10f, 10f, 200f, 200f), PlayerTile.ToString());
    }
    
    [Serializable]
    public class TilePrefab
    {
        public GameObject prefab;
        [SerializeField, Range(1, 5)] public int weight;
    }
}
