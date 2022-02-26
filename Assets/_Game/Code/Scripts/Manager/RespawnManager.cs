using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPos;
    
    public void Respawn()
    {
        GameManager.Instance.controlledEntity.transform.position = respawnPos.position;
    }
}
