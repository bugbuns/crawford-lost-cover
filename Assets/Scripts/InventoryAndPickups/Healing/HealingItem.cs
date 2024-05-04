using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class HealingItem: MonoBehaviour, IInteractable
{
    public enum HealingItemType
    {
        Bandages,
        None
    }

    public HealingItemType itemType;
    public InventoryManager invManager;
    public int quantity;



    private bool inRangeToPickup = false;

  
    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case HealingItemType.Bandages: return ItemAssets.Instance.Bandages;
            case HealingItemType.None: return ItemAssets.Instance.Fists;
            

        }
    }

    public int HealAmount()
    {
        switch(itemType)
        {
            default:
            case HealingItemType.Bandages: return 50;
            case HealingItemType.None: return 0;
        }
    }
    //Interaction
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        invManager.SetHeals(this);
        gameObject.SetActive(false);
        return true;
    }
}
