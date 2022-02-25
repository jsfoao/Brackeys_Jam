using UnityEngine;

public class DrawWireframe : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z));
    }
}
