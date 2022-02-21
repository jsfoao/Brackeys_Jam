using UnityEngine;

public class FramerateChanger : MonoBehaviour
{
    [SerializeField, Range(30f, 200f)] int defaultFramerate;
    [SerializeField, Range(30f, 200f)] int otherFramerate;
    [SerializeField] private bool active;
    

    void Update()
    {
        if (!active)
        {
            Application.targetFrameRate = defaultFramerate;
            
        }
        else
        {
            Application.targetFrameRate = otherFramerate;
        }
    }
}