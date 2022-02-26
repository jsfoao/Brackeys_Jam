using UnityEngine;

public class EventDebugger : MonoBehaviour
{
    public void LogEvent(string log)
    {
        Debug.Log(gameObject.name + ": " + log);
    }
}
