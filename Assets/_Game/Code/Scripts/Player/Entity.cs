using UnityEngine;

public class Entity : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance.controlledEntity == null)
        {
            GameManager.Instance.controlledEntity = this;
        }
    }
}
