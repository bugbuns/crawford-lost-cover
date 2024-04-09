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

    private Animator _animator;

    private Vector3 temp;

    public bool isCrouching;

    private CamControl cameraController;
    private Quaternion targetRotation;
    
    private void Awake()
    {
        //CamControl
        cameraController = Camera.main.GetComponent<CamControl>();

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

        float moveAmount = Mathf.Abs(h) + Mathf.Abs(v);

        var moveInput = (new Vector3(h, 0, v)).normalized;

        var moveDir = cameraController.PlanarRotation * moveInput;

        if (moveAmount > 0)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

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
<<<<<<< HEAD
            moveSpeed = moveSpeed / 2;
=======
>>>>>>> parent of 3a5a545 (Harvey Crouches and movement is slowed when crouched)
            _animator.SetBool("isCrouching", true);
        }
        else if (isCrouching == true && _playerInput.actions["Crouch"].WasPressedThisFrame())
        {
            isCrouching = false;
<<<<<<< HEAD
            moveSpeed = moveSpeed * 2;
=======
>>>>>>> parent of 3a5a545 (Harvey Crouches and movement is slowed when crouched)
            _animator.SetBool("isCrouching", false);
        }
    }
}

