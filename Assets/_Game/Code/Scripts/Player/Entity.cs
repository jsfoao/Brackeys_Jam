using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float awarenessMin = 1.5f;
    [SerializeField] private float awarenessMax = 10f;
    [NonSerialized] public float awareness;
    public float awarenessDrain;
    
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
    
    #if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Label(new Rect(10f, 200f, 200f, 200f), $"ENTITY");
        GUI.Label(new Rect(10f, 220f, 200f, 200f), $"Awareness {awareness}");
    }
    #endif
}
