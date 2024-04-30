using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.Experimental.GraphView;
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
    private float tempH;
    private float tempV;
    [SerializeField] private float _dodgeSpeedMultiplier = 1.333333f;
    [SerializeField] private float _dodgeTime;
    private float _dodgeTimer;

    private Animator _animator;

    private Vector3 temp;

    private bool beginningCrouch;
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
        if (!isDodging)
        {
            _dodgeTimer = 0;
            
            h = _playerInput.actions["Movement"].ReadValue<Vector2>().x;
            v = _playerInput.actions["Movement"].ReadValue<Vector2>().y;

            _movementInput = _playerInput.actions["Movement"].ReadValue<Vector2>();
            _animator.SetFloat("hzInput", _movementInput.x, 0.1f, Time.deltaTime); //Animations blend together better with float and Time.deltaTime
            _animator.SetFloat("vInput", _movementInput.y, 0.1f, Time.deltaTime);

            if (_movementInput.magnitude > .2)
            {
                _animator.SetBool("isWalking", true);
            }
            
            Vector3 move = new Vector3(h, 0f, v) * (moveSpeed * Time.deltaTime);
            transform.Translate(move, Space.Self);
            gameObject.layer = 3;
        }
        else
        {
            _dodgeTimer += Time.deltaTime;
            Vector3 move = new Vector3(h, 0f, v) * (moveSpeed * Time.deltaTime * _dodgeSpeedMultiplier);
            transform.Translate(move, Space.Self);
            gameObject.layer = 2;

            if (_dodgeTimer > _dodgeTime)
            {
                isDodging = false;
            }
        }

        _animator.SetFloat("hzInput", h, 0.1f, Time.deltaTime); //Animations blend together better with float and Time.deltaTime
        _animator.SetFloat("vInput", v, 0.1f, Time.deltaTime);

        if (_playerInput.actions["Crouch"].triggered)
        {
            beginningCrouch = true;
        }

        if (_playerInput.actions["Dodge"].triggered)
        {
            isDodging = true;
            StartCoroutine(changeLayerForDodge());
            _animator.SetTrigger("isDodging");
            
        }
        
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
        
        _animator.SetBool("isCrouching", isCrouching);

        //Debug.Log(_movementInput);
    }

    void FixedUpdate()
    {
        if (!isCrouching && beginningCrouch)
        {
            isCrouching = true;

            moveSpeed /= 2;

            beginningCrouch = false;
        }
        else if (isCrouching && beginningCrouch)
        {
            isCrouching = false;

            moveSpeed *= 2;
            
            beginningCrouch = false;
        }
    }

    void Melee()
    {
        int attackMode = UnityEngine.Random.Range(1, 4);
        _animator.SetTrigger("Punch " + attackMode);
        Debug.Log("Punch");
    }

    IEnumerator changeLayerForDodge()
    {
        gameObject.layer = 2;
        Debug.Log("Safe");
        yield return new WaitForSeconds(.5f);
        Debug.Log("Unsafe");
        gameObject.layer = 3;

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

