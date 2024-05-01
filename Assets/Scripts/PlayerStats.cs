using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStats:MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        activeMelee = new MeleeItem();
        activeMelee.itemType = MeleeItem.MeleeItemType.Fists;

    }

    public MeleeItem activeMelee;
    public RangedItem activeRanged;
    public HealingItem ActiveHealingItem;
    
    public int health;
    public int pistolAmmo;
    public int shotgunAmmo;
    public int TommyGunAmmo;
    

    public int maxPistolAmmo;
    public int maxShotgunAmmo;
    public int maxTommyGunAmmo;

    public void decreaseAmmo(int ammoDec)
    {
        switch (activeRanged.itemType)
        {
            case RangedItem.RangedItemType.Pistol:
                pistolAmmo -= ammoDec;
                return;
            case RangedItem.RangedItemType.Shotgun:
                shotgunAmmo -= ammoDec;
                return;
            case RangedItem.RangedItemType.TommyGun:
                TommyGunAmmo -= ammoDec;
                return;
            
        }
    }

    
}
