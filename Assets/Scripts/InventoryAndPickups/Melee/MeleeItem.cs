using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;


public class MeleeItem: MonoBehaviour
{
    public enum MeleeItemType
    {
        Crowbar,
        Bat
    }

    public MeleeItemType itemType;
    public int meleeHealth;
    public InventoryManager invManager;

    private void Awake()
    {
        meleeHealth = 100;
    }
    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case MeleeItemType.Crowbar: return ItemAssets.Instance.Item1Sprite;
            case MeleeItemType.Bat: return ItemAssets.Instance.Item2Sprite;
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            invManager.SetMeleeWeapon(this);
            Destroy(this.gameObject);
        }
    }
}
