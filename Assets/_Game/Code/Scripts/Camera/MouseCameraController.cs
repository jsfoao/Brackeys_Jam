using System;
using UnityEngine;

[RequireComponent(typeof(OrbitCamera))]
public class MouseCameraController : MonoBehaviour
{
    private OrbitCamera _orbitCamera;
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [SerializeField] private bool invertX;
    [SerializeField] private bool invertY;

    private bool _active;
    private float _timer;


    private void Update()
    {
        if (!_active)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _active = true;
            }
            return;
        }
        
        // Inverting axis
        int invertMultX = invertX ? -1 : 1;
        int invertMultY = invertY ? -1 : 1;

        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        _orbitCamera.Angles += new Vector2(mouseInput.x * sensX * invertMultX, mouseInput.y * sensY * invertMultY);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _orbitCamera = GetComponent<OrbitCamera>();
        _timer = 0.1f;
    }
    
    private void OnGUI()
    {
        Vector2 labelSize = new Vector2(200f, 200f);
        Vector2 labelPos = new Vector2(10f, 10f);
        Vector2 offset = new Vector2(0f, 15f);
    }
}
