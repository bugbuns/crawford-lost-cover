using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.InputSystem;
using UnityEngine;


public class RangedItem: MonoBehaviour
{
    public enum RangedItemType
    {
       Pistol,
       Shotgun,
       TommyGun
    }

    public RangedItemType itemType;
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
            case RangedItemType.Pistol: return ItemAssets.Instance.Item1Sprite;
            case RangedItemType.Shotgun: return ItemAssets.Instance.Item2Sprite;
            case RangedItemType.TommyGun: return ItemAssets.Instance.Item3Sprite;
        }
    }

    public int GetAmmo()
    {
        switch (itemType)
        {
            default:
            case RangedItemType.Pistol: return PlayerStats.Instance.pistolAmmo;
            case RangedItemType.Shotgun: return PlayerStats.Instance.shotgunAmmo;
            case RangedItemType.TommyGun: return PlayerStats.Instance.TommyGunAmmo;    
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
            invManager.SetRangedWeapon(this);
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
