using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class RangedItem: MonoBehaviour
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            invManager.SetRangedWeapon(this);
            Destroy(this.gameObject);
        }
    }
}
