using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class RangedItem: MonoBehaviour, IInteractable
{
    public enum RangedItemType
    {
       Pistol,
       Revolver,
       Shotgun,
       TommyGun
    }

    public RangedItemType itemType;
    public InventoryManager invManager;
    
    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case RangedItemType.Pistol: return ItemAssets.Instance.Pistol;
            case RangedItemType.Revolver: return ItemAssets.Instance.Revolver;
            case RangedItemType.Shotgun: return ItemAssets.Instance.Shotgun;
            case RangedItemType.TommyGun: return ItemAssets.Instance.TommyGun;
        }
    }

    public int GetAmmo()
    {
        switch (itemType)
        {
            default:
            case RangedItemType.Pistol: return PlayerStats.Instance.pistolAmmo;
            case RangedItemType.Revolver: return PlayerStats.Instance.pistolAmmo;
            case RangedItemType.Shotgun: return PlayerStats.Instance.shotgunAmmo;
            case RangedItemType.TommyGun: return PlayerStats.Instance.TommyGunAmmo;    
        }
    }

    public int GetMagazineSize()
    {
        switch (itemType)
        {
            default:
            case RangedItemType.Pistol: return 8;
            case RangedItemType.Revolver: return 6;
            case RangedItemType.Shotgun: return 5;
            case RangedItemType.TommyGun: return 25;
        }
    }

    public int GetDamage()
    {
        switch (itemType)
        {
            default:
            case RangedItemType.Pistol: return 2;
            case RangedItemType.Revolver: return 4;
            case RangedItemType.Shotgun: return 6;
            case RangedItemType.TommyGun: return 3; 
        }
    }

    public float GetRange()
    {
        switch (itemType)
        {
            default:
            case RangedItemType.Pistol: return 15f;
            case RangedItemType.Revolver: return 15f;
            case RangedItemType.Shotgun: return 7f;
            case RangedItemType.TommyGun: return 15f;
        }
    }

    //Interaction System
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        invManager.SetRangedWeapon(this);
        gameObject.SetActive(false);
        //Destroy(this.gameObject);
        return true;
    }
}
