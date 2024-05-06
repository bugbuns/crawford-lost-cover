using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Animations;
//using UnityEditor.Experimental.GraphView;
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

    public Animator _animator;

    private Vector3 temp;

    private bool beginningCrouch;
    public bool isCrouching;
    public bool isDodging;
    public HUDManager _HUDManager;
    
    //Camera Variables
    private CamControl cameraController;
    private Quaternion targetRotation;

    public bool weaponEquipped;

    public int currentEquip;
    public bool aiming = false;
    

    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponentInChildren<Animator>();
        
        isCrouching = false;
        weaponEquipped = false;
        currentEquip = 1;
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
        
        //Keep track of if melee or range is equipped
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!_animator.GetBool("hasMeleeEquipped"))
            {
                currentEquip = 1;
                _animator.SetTrigger("isRangeUnequip");
                _animator.SetTrigger("isMeleeEquipping");
            }

            _animator.SetBool("hasRangeEquipped", false);
                _animator.SetBool("hasMeleeEquipped", true);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!_animator.GetBool("hasRangeEquipped"))
            {
                _animator.SetTrigger("isMeleeUnequip");
                _animator.SetTrigger("isRangeEquipping");
            }

            _animator.SetBool("hasMeleeEquipped", false);
            _animator.SetBool("hasRangeEquipped", true);
            currentEquip = 2;
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

        //Melee animation tracker
        if (PlayerStats.Instance.activeMelee.itemType == MeleeItem.MeleeItemType.Fists)
        {
            _animator.SetBool("hasCrowPipe", false);
            weaponEquipped = false;
        }
        else if (PlayerStats.Instance.activeMelee.itemType == MeleeItem.MeleeItemType.Crowbar)
        {
            _animator.SetBool("hasCrowPipe", true);
            weaponEquipped = true;
        }
        else if (PlayerStats.Instance.activeMelee.itemType == MeleeItem.MeleeItemType.Bat)
        {
            _animator.SetBool("hasBat", true);
            weaponEquipped = true;
        }

        //Range animation Tracker
        if (PlayerStats.Instance.activeRanged == null)
        {

        }
        else if (PlayerStats.Instance.activeRanged.itemType == RangedItem.RangedItemType.Pistol)
        {
            _animator.SetBool("hasPistol", true);
        }
        else if (PlayerStats.Instance.activeRanged.itemType == RangedItem.RangedItemType.Revolver)
        {
            _animator.SetBool("hasPistol", true);
        }
        else if (PlayerStats.Instance.activeRanged.itemType == RangedItem.RangedItemType.Shotgun)
        {
            _animator.SetBool("hasShotgun", true);
        }
        else if (PlayerStats.Instance.activeRanged.itemType == RangedItem.RangedItemType.TommyGun)
        {
            _animator.SetBool("hasRifle", true);
        }

        //Does Attacks
        
        if (currentEquip == 1)
        {
            Melee();
        }
        else
        {
            Range();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentEquip == 2)
            {
                _animator.SetTrigger("Reload");
            }
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (weaponEquipped == false)
            {
                int attackMode = UnityEngine.Random.Range(1, 4);
                _animator.SetTrigger("Punch " + attackMode);
                //Debug.Log("Punch");
            }
            else if (weaponEquipped == true)
            {
                int attackMode = UnityEngine.Random.Range(1, 4);
                _animator.SetTrigger("Swing " + attackMode);
                Debug.Log("Swing");
            }
        }
    }

    void Range()
    {
        if (Input.GetMouseButton(1))
        {
            _animator.SetBool("isAiming", true);
            aiming = true;
        }
        else
        {
            _animator.SetBool("isAiming", false);
            aiming = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (aiming = true)
            {
                _animator.SetTrigger("shooting");
            }
            
        }
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

