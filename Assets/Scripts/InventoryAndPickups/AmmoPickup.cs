using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class AmmoPickup : MonoBehaviour
{
    public int ammoCount;
    public ammoType type;
    public PlayerControls input;
    public InventoryManager invManager;
    private InputAction pickUp;
    private bool inRangeToPickup = false;
    void Awake()
    {
        ammoCount = Random.Range(1, 11);
        input = new PlayerControls();
    }
    public enum ammoType
    {
        pistolAmmo,
        shotgunAmmo,
        tommygunAmmo
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
            if (type==ammoType.pistolAmmo)
            {
                PlayerStats.Instance.pistolAmmo += ammoCount;
                
            }
            else if (type==ammoType.shotgunAmmo)
            {
                PlayerStats.Instance.shotgunAmmo += ammoCount;
                
            }
            else if (type == ammoType.tommygunAmmo)
            {
                PlayerStats.Instance.pistolAmmo += ammoCount;
            }
            invManager.refreshInventory();
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
