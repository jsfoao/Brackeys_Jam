using System;
using UnityEngine;
using UnityEngine.UI;

public class UIAwareness : MonoBehaviour
{
    [SerializeField, Range(0,1)] public float slider;

    private RectTransform _rectTransform;
    private Image _image;
    private void Update()
    {
        float awareness = GameManager.Instance.controlledEntity.awareness;
        float awarenessMax = GameManager.Instance.controlledEntity.awarenessMax;
        _rectTransform.sizeDelta = new Vector2(Screen.width * awareness / awarenessMax, 15f);
    }

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
}
