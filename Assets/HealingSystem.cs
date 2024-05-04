using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealingSystem : MonoBehaviour
{
    private PlayerControls input;
    public HUDManager _hudManager;
    public InventoryManager _InventoryManager;
    public bool isHealing;
    private InputAction heal;
    void Awake()
    {
        input = new PlayerControls();
    }

    public void Heal(InputAction.CallbackContext context)
    {
        if (PlayerStats.Instance.health != 100&&!isHealing&&_hudManager.healsActive)
        {
            if (PlayerStats.Instance.ActiveHealingItem.itemType == HealingItem.HealingItemType.Bandages)
            {
                StartCoroutine(bandageHeal());
            }
            else
            {
                PlayerStats.Instance.health += PlayerStats.Instance.ActiveHealingItem.HealAmount();
                if (PlayerStats.Instance.health > 100) PlayerStats.Instance.health = 100;
            }
            
            PlayerStats.Instance.ActiveHealingItem.quantity--;
            
            _hudManager.refreshHealingHud();
        }
    }

    IEnumerator bandageHeal()
    {
        //Animation
        isHealing = true;
        Debug.Log("Begin Healing");
        yield return new WaitForSeconds(5f);
        PlayerStats.Instance.health += PlayerStats.Instance.ActiveHealingItem.HealAmount();
        if (PlayerStats.Instance.health > 100) PlayerStats.Instance.health = 100;
        isHealing = false;
        if (PlayerStats.Instance.ActiveHealingItem.quantity <= 0)
        {
            _InventoryManager.removeHealingItem();
            HealingItem none = new HealingItem();
            none.itemType = HealingItem.HealingItemType.None;
            _InventoryManager.SetHeals(none);
        }
        _hudManager.refreshHealingHud();
        Debug.Log("End Healing");
    }

    private void OnEnable()
    {
        heal = input.Player.FireTap;
        heal.Enable();
        heal.performed += Heal;
    }

    private void OnDisable()
    {
        heal.Disable();
    }
}
