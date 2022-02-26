using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TileSpawner : MonoBehaviour
{
    public List<TilePrefab> prefabs;
    private List<GameObject> list = new List<GameObject>();
    private GameObject spawnedChunk;
    private SpawnGenerator spawnGenerator;
    public int row;
    public Material glitchedMaterial;
    public Material regularMaterial;
    private Renderer _renderer;
    private Collider _collider;
    public bool isReal;

    private void Start()
    {
        spawnGenerator = FindObjectOfType<SpawnGenerator>();
        foreach (var t in prefabs)
        {
            int i = 0;
            while (i < t.weight)
            {
                list.Add(t.prefab);
                i += 1;
            }
        }
        spawnedChunk = Instantiate(list[Random.Range(0, list.Count)], transform.position, Quaternion.identity);
        _renderer = spawnedChunk.GetComponentInChildren<Renderer>();
        _collider = spawnedChunk.GetComponentInChildren<Collider>();
        _collider.enabled = isReal;

    }

    void Update()
    {
        float distanceToPlayer = spawnGenerator.PlayerTile - row;
        if (distanceToPlayer > spawnGenerator.mapLength)
        {
            Destroy(spawnedChunk);
            Destroy(this);
        }

        if (distanceToPlayer >= -GameManager.Instance.controlledEntity.range && !isReal)
        {
            _renderer.material = glitchedMaterial;
        }
        else
        {
            _renderer.material = regularMaterial;
        }
    }
}

[Serializable]
public class TilePrefab
{
    public GameObject prefab;
    [SerializeField, Range(1, 5)] public int weight;
}