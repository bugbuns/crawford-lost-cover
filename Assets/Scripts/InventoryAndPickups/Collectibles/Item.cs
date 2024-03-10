using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.InputSystem;
using UnityEngine;


public class Item: MonoBehaviour
{
    public enum ItemType
    {
        Item1,
        Item2,
        Item3
    }

    public ItemType itemType;
    public InventoryManager invManager;
    
    public PlayerControls input;
    private InputAction pickUp;
    private bool inRangeToPickup = false;

    private void Awake()
    {
        input = new PlayerControls();
    }
    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.Item1: return ItemAssets.Instance.Item1Sprite;
            case ItemType.Item2: return ItemAssets.Instance.Item2Sprite;
            case ItemType.Item3: return ItemAssets.Instance.Item3Sprite;
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
            Inventory.Instance.AddItem(this);
            invManager.AddItem(this);
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
