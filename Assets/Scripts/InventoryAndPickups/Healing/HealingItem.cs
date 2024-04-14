using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class HealingItem: MonoBehaviour, IInteractable
{
    public enum HealingItemType
    {
        Medkit
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
            case HealingItemType.Medkit: return ItemAssets.Instance.Medkit;
            
        }
    }
    //Interaction
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        invManager.SetHeals(this);
        Destroy(this.gameObject);
        return true;
    }
}
