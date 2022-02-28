using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public float awarenessMin = 1.5f;
    [SerializeField] public float awarenessMax = 10f;
    [NonSerialized] public float awareness;
    public float awarenessDrain;
    public int maxScore = 0;
    
    private void Start()
    {
        if (GameManager.Instance.controlledEntity == null)
        {
            GameManager.Instance.controlledEntity = this;
        }

        awareness = awarenessMax;
    }

    private void Update()
    {
        awareness -= Time.deltaTime * awarenessDrain;
        awareness = Mathf.Clamp(awareness, awarenessMin, awarenessMax);
    }
}
