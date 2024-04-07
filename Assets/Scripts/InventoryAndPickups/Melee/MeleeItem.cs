using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class MeleeItem: MonoBehaviour,IInteractable
{
    public enum MeleeItemType
    {
        Fists,
        Crowbar,
        Bat
    }

    public MeleeItemType itemType;
    public int meleeHealth;
    public InventoryManager invManager;



    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case MeleeItemType.Fists: return ItemAssets.Instance.Item3Sprite;
            case MeleeItemType.Crowbar: return ItemAssets.Instance.Item1Sprite;
            case MeleeItemType.Bat: return ItemAssets.Instance.Item2Sprite;
            
        }
    }

    public int GetDamage()
    {
        switch(itemType)
        {
            default:
            case MeleeItemType.Fists: return 1;
            case MeleeItemType.Crowbar: return 3;
            case MeleeItemType.Bat: return 2;
            
        }
    }
    
    //Interaction
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        invManager.SetMeleeWeapon(this);
        Destroy(this.gameObject);
        return true;
    }
}
