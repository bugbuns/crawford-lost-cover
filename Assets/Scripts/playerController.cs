using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    // Serialized variables
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private GameObject _camFocus;
    
    // Basic movement variables
    private Vector3 _movementInput;
    private Vector3 _forward;
    private Vector3 _right;
    private Rigidbody _rigidBody;
    private Vector3 _camForward;
    private Vector3 _camRight;
    
    // To manage jumping (and not jumping infinitely)
    private bool _isJumping;
    private bool _isMidair = true;
    
    // Input system
    private PlayerInput _playerInput;

    private Vector3 temp;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _movementInput = _playerInput.actions["Movement"].ReadValue<Vector2>();
        
        //Debug.Log(_movementInput);
    }

    void FixedUpdate()
    {
        _camForward = _camFocus.transform.forward - new Vector3(0, _camFocus.transform.forward.y, 0);
        _camRight = _camFocus.transform.right - new Vector3(0, _camFocus.transform.right.y, 0);
        _camForward.Normalize();
        _camRight.Normalize();
        
        _forward = _movementInput.normalized.y * _movementSpeed * _camForward;
        _right = _movementInput.normalized.x * _movementSpeed * _camRight;

        transform.eulerAngles = _camForward;
        
        _rigidBody.velocity = _forward + _right + new Vector3(0, _rigidBody.velocity.y, 0);
    }
}

