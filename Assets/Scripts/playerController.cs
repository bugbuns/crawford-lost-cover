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

    // Basic movement variables
    private Vector3 _movementInput;



    // Input system
    private PlayerInput _playerInput;
    private float h;
    private float v;

    private Animator _animator;

    private Vector3 temp;

    public bool isCrouching;
    public bool isDodging;
    public HUDManager _HUDManager;
    

    private CamControl cameraController;
    private Quaternion targetRotation;


    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Melee();
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
            //gameObject.GetComponent<Rigidbody>().velocity =new Vector3();
            //_animator.SetTrigger("isDodging");

        }
    }

    void Melee()
    {
        int attackMode = UnityEngine.Random.Range(1, 4);
        _animator.SetTrigger("Punch " + attackMode);
        Debug.Log("Punch");
    }

    public void takeDamage(int damage)
    {
        PlayerStats.Instance.health -= damage;
        _HUDManager.setHealthBar();
        if (PlayerStats.Instance.health <= 0)
        {
            gameOver();
        }
        
    }

    public void gameOver()
    {
        //Game Over
    }

    
}

