using UnityEngine;

public class Entity : MonoBehaviour
{
    public int range;
    
    private void Start()
    {
        if (GameManager.Instance.controlledEntity == null)
        {
            GameManager.Instance.controlledEntity = this;
        }
    }
}
