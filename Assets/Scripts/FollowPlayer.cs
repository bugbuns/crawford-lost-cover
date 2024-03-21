using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private float _yOffset;
    
    [SerializeField] private float _camRotationMult;

    [SerializeField] private float _lerpTime = .35f;

    [SerializeField] private float _angleClampVertical;
    [SerializeField] private bool _cameraInverted = false;

    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;

    public Quaternion _rotation;
    private Vector3 _lookInput;
    private Vector3 _previousLookInput;
    void Start ()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }
 
    void Update ()
    {
        transform.position = _player.transform.position + new Vector3(0, _yOffset, 0);
        _previousLookInput = _lookInput;
        _lookInput = _playerInput.actions["Look"].ReadValue<Vector2>();
        if (!_cameraInverted)
        {
            _lookInput.y *= -1;
        }
        _lookInput *= _camRotationMult;

        _lookInput = Vector3.Lerp(_previousLookInput, _lookInput, _lerpTime);

        var cameraAngles = transform.localEulerAngles;
        var cameraVerticalAngle = transform.localEulerAngles.x;

        if (cameraVerticalAngle > 180 && cameraVerticalAngle < 180 + _angleClampVertical + 20)
        {
            cameraAngles.x = 180 + _angleClampVertical + 20;
        }
        else if (cameraVerticalAngle < 180 && cameraVerticalAngle > 180 - _angleClampVertical)
        {
            cameraAngles.x = 180 - _angleClampVertical;
        }

        transform.localEulerAngles = cameraAngles;
    }

    private void FixedUpdate()
    {
        _rotation = Quaternion.Euler(_rigidbody.rotation.eulerAngles.x + Mathf.Deg2Rad * _lookInput.y, _rigidbody.rotation.eulerAngles.y
                                       + Mathf.Deg2Rad * _lookInput.x, 0);

        _rigidbody.rotation = _rotation;
    }
}