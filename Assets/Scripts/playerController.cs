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
    public float rotationSpeed = 15;

    
    // Basic movement variables
    private Vector3 _movementInput;
    private Rigidbody _rigidBody;
    [SerializeField] float moveSpeed = 5f;

    

    // Input system
    private PlayerInput _playerInput;

    private Animator _animator;

    private Vector3 temp;

    public bool isCrouching;
    
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

        isCrouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        var moveInput = (new Vector3(h, 0, v)).normalized;

        transform.position += moveInput * moveSpeed * Time.deltaTime;

        _movementInput = _playerInput.actions["Movement"].ReadValue<Vector2>();

        _animator.SetFloat("hzInput", _movementInput.x, 0.1f, Time.deltaTime); //Animations blend together better with float and Time.deltaTime
        _animator.SetFloat("vInput", _movementInput.y, 0.1f, Time.deltaTime);

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
        
        if (isCrouching == false && _playerInput.actions["Crouch"].WasPressedThisFrame())
        {
            isCrouching = true;
            _movementSpeed = _movementSpeed / 2;
            _animator.SetBool("isCrouching", true);
        }
        else if (isCrouching == true && _playerInput.actions["Crouch"].WasPressedThisFrame())
        {
            isCrouching = false;
            _movementSpeed = _movementSpeed * 2;
            _animator.SetBool("isCrouching", false);
        }
    }

    
}

