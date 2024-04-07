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

    public int GetMagazineSize()
    {
        switch (itemType)
        {
            default:
            case RangedItemType.Pistol: return 8;
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
            case RangedItemType.Shotgun: return 5;
            case RangedItemType.TommyGun: return 3; 
        }
    }

    public float GetRange()
    {
        switch (itemType)
        {
            default:
            case RangedItemType.Pistol: return 15f;
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
        Destroy(this.gameObject);
        return true;
    }
}
