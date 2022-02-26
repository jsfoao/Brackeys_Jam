using UnityEngine;

[RequireComponent(typeof(OrbitCamera))]
public class AnimOrbit : MonoBehaviour
{
    [SerializeField] private float angularSpeed;
    [SerializeField] private int direction;
    
    private OrbitCamera _orbitCamera;
    private void Update()
    {
        _orbitCamera.Angles += Time.deltaTime * angularSpeed * new Vector2(direction, 0);
    }

    private void Start()
    {
        _orbitCamera = GetComponent<OrbitCamera>();
    }

    private void OnValidate()
    {
        direction = Mathf.Clamp(direction, -1, 1);
    }
}
