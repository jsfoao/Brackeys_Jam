using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Control[] inputs;
    
    private void Update()
    {
        foreach (Control control in inputs)
        {
            if (Input.GetKeyDown(control.KeyCode))
            {
                control.Event.Invoke();
            }
        }
    }
}

[Serializable]
public class Control
{
    public KeyCode KeyCode;
    public UnityEvent Event;

    Control(KeyCode keyCode)
    {
        KeyCode = keyCode;
    }
}