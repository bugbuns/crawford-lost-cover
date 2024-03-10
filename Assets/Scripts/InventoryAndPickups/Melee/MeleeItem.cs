using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;


public class MeleeItem: MonoBehaviour
{
    public enum MeleeItemType
    {
        Crowbar,
        Bat
    }

    public MeleeItemType itemType;
    public int meleeHealth;
    public InventoryManager invManager;

    
    public PlayerControls input;
    private InputAction pickUp;
    private bool inRangeToPickup = false;
    private void Awake()
    {
        meleeHealth = 100;
        input = new PlayerControls();
    }
    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case MeleeItemType.Crowbar: return ItemAssets.Instance.Item1Sprite;
            case MeleeItemType.Bat: return ItemAssets.Instance.Item2Sprite;
            
        }
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            inRangeToPickup = true;
        }
    }
  
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            inRangeToPickup = false;
        }
    }

    public void pickUpItem(InputAction.CallbackContext context)
    {
        if (inRangeToPickup)
        {
            invManager.SetMeleeWeapon(this);
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        pickUp = input.Player.PickupInteract;
        pickUp.Enable();
        pickUp.performed += pickUpItem;
    }

    private void OnDisable()
    {
        pickUp.Disable();
    }
}
