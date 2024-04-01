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
    Vector3 moveDirection;
    InputManager inputManager;


    Transform cameraObject;
    

    // Input system
    private PlayerInput _playerInput;

    private Animator _animator;

    private Vector3 temp;

    public bool isCrouching;
    
    void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraObject = Camera.main.transform;
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

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.vertInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horiInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * _movementSpeed;

        Vector3 movementVelocity = moveDirection;
        _rigidBody.velocity = movementVelocity;

    }


    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.vertInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horiInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}

