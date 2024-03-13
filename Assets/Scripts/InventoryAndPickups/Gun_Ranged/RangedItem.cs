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
