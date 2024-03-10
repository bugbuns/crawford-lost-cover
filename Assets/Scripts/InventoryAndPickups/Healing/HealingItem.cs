using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.InputSystem;
using UnityEngine;


public class HealingItem: MonoBehaviour
{
    public enum HealingItemType
    {
        Medkit
    }

    public HealingItemType itemType;
    public InventoryManager invManager;
    public int quantity;
    
    
    public PlayerControls input;
    private InputAction pickUp;
    private bool inRangeToPickup = false;

    void Awake()
    {
        input = new PlayerControls();
    }
    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case HealingItemType.Medkit: return ItemAssets.Instance.Item1Sprite;
            
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
            invManager.SetHeals(this);
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
