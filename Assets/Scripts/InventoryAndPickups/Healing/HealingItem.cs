using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class HealingItem: MonoBehaviour
{
    public enum HealingItemType
    {
        Medkit
    }

    public HealingItemType itemType;
    public InventoryManager invManager;
    public int quantity;

    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case HealingItemType.Medkit: return ItemAssets.Instance.Item1Sprite;
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            invManager.SetHeals(this);
            Destroy(this.gameObject);
        }
    }
}
