using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class Item: MonoBehaviour
{
    public enum ItemType
    {
        Item1,
        Item2,
        Item3
    }

    public ItemType itemType;
    public InventoryManager invManager;

    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.Item1: return ItemAssets.Instance.Item1Sprite;
            case ItemType.Item2: return ItemAssets.Instance.Item2Sprite;
            case ItemType.Item3: return ItemAssets.Instance.Item3Sprite;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Inventory.Instance.AddItem(this);
            invManager.AddItem(this);
            Destroy(this.gameObject);
        }
    }
}
