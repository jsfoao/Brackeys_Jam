using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [NonSerialized] public int range;
    private float awareness = 100;
    public float awarenessDrain;
    
    private void Start()
    {
        if (GameManager.Instance.controlledEntity == null)
        {
            GameManager.Instance.controlledEntity = this;
        }
    }

    private void Update()
    {
        awareness -= Time.deltaTime * awarenessDrain;
        awareness = Mathf.Clamp(awareness, 0, 100);
        range = Mathf.CeilToInt(awareness / 10);
    }
    
    #if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Label(new Rect(10f, 200f, 200f, 200f), $"ENTITY");
        GUI.Label(new Rect(10f, 220f, 200f, 200f), $"Awareness {awareness}");
        GUI.Label(new Rect(10f, 240f, 200f, 200f), $"Range {range}");
    }
    #endif
}
