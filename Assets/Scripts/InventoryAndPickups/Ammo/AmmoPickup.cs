using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmmoPickup : MonoBehaviour,IInteractable
{
    public int ammoCount;
    public ammoType type;
    public InventoryManager invManager;
    void Awake()
    {
        ammoCount = Random.Range(1, 11);
    }
    public enum ammoType
    {
        pistolAmmo,
        shotgunAmmo,
        tommygunAmmo
    }
    

//Interaction
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
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
        return true;
    }
}
