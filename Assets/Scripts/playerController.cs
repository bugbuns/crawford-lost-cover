using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    // Serialized variables
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private GameObject _camFocus;
    
    // Basic movement variables
    private Vector3 _movementInput;
    private Vector3 _forward;
    private Vector3 _right;
    private Rigidbody _rigidBody;
    private Vector3 _camForward;
    private Vector3 _camRight;
    
    // Input system
    private PlayerInput _playerInput;

    private Animator _animator;

    private Vector3 temp;
    
    void Awake()
    {
        //Create and Setup Inventory
       
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _movementInput = _playerInput.actions["Movement"].ReadValue<Vector2>();

        if (_movementInput.x != 0)
        {
            _animator.SetFloat("horiz", _movementInput.x / Mathf.Abs(_movementInput.x));
        }
        else
        {
            _animator.SetFloat("horiz", 0);
        }

        if (_movementInput.y != 0)
        {
            _animator.SetFloat("vert", _movementInput.y / Mathf.Abs(_movementInput.y));
        }
        else
        {
            _animator.SetFloat("vert", 0);
        }

        if (_movementInput != Vector3.zero)
        {
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }

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

        transform.forward = _camForward;

        if (_movementInput.magnitude > .1f)
        {
            _rigidBody.velocity = _forward + _right + new Vector3(0, _rigidBody.velocity.y, 0);
        }
    }
}

