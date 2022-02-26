using System;
using UnityEngine;

public class FPLook : MonoBehaviour 
{
    public float Sensitivity
    {
        get => sensitivity;
        set => sensitivity = value;
    }
    
    [Header("Camera Look")]
    [Range(0.1f, 10f)][SerializeField] float sensitivity = 2f;
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

    [Header("Camera Controls")] 
    [NonSerialized] public float tiltAngle;
    [SerializeField] public float maxTiltAngle;
    [SerializeField] private float tiltSpeed;
    
    
    private float currentTilt;
    
    Vector2 _rotation = Vector2.zero;
    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";
    
    private Transform _camTransform;

    private float _startTimer;

    private void LookRotation()
    {
        _rotation.x += Input.GetAxis(xAxis) * sensitivity;
        _rotation.y += Input.GetAxis(yAxis) * sensitivity;
        _rotation.y = Mathf.Clamp(_rotation.y, -yRotationLimit, yRotationLimit);
        
        Quaternion xQuaternion = Quaternion.AngleAxis(_rotation.x, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(_rotation.y, Vector3.left);

        _camTransform.localRotation = xQuaternion * yQuaternion;   
    }

    private void TiltRotation()
    {
        _camTransform.localRotation *= Quaternion.Euler(0f, 0f, currentTilt);
    }

    void Update()
    {
        if (_startTimer > 0)
        {      
            _startTimer -= Time.deltaTime;
            return;
        }
        
        LookRotation();
        TiltRotation();
        currentTilt = Mathf.Lerp(currentTilt, tiltAngle, tiltSpeed * Time.deltaTime);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _startTimer = 0.5f;
        _camTransform = GetComponentInChildren<Camera>().transform;
    }
    
    private void OnValidate()
    {
        maxTiltAngle = Mathf.Clamp(maxTiltAngle, 0f, 90f);
        tiltAngle = Mathf.Clamp(tiltAngle, -maxTiltAngle, maxTiltAngle);
    }
}