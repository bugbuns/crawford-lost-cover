using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    // Serialized variables
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;

    // Basic movement variables
    private Vector3 _movementInput;
    private Rigidbody _rigidBody;
    


    // Input system
    private PlayerInput _playerInput;
    private float h;
    private float v;

    private Animator _animator;

    private Vector3 temp;

    public bool isCrouching;
    public bool isDodging;
    

    private CamControl cameraController;
    private Quaternion targetRotation;
    
    
    private void Awake()
    {
        //CamControl
        cameraController = Camera.main.GetComponent<CamControl>();
        
       


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
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0f, v) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.Self);

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

            moveSpeed = moveSpeed / 2;

            _animator.SetBool("isCrouching", true);
        }
        else if (isCrouching == true && _playerInput.actions["Crouch"].WasPressedThisFrame())
        {
            isCrouching = false;

            moveSpeed = moveSpeed * 2;

            _animator.SetBool("isCrouching", false);
        }

        if (_playerInput.actions["Dodge"].WasPressedThisFrame()) 
        {
            _animator.SetTrigger("isDodging");
            
        }
    }

    
}

