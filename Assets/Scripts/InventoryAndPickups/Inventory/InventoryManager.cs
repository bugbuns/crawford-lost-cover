using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

  
  
  public InventorySlot[] _inventorySlots;
  public InventorySlot meleeWeaponSlot;
  public InventorySlot rangedWeaponSlot;
  public InventorySlot healingItemSlot;
  public GameObject inventoryItemPrefab;
  public GameObject meleeInventoryItemPrefab;
  public TextMeshProUGUI meleeHealthText;
  public GameObject rangeInventoryItemPrefab;
  public TextMeshProUGUI ammoText;
  public GameObject healsInventoryItemPrefab;
  public TextMeshProUGUI healsQuantityText;
  public GameObject inventory;
  public PlayerControls input;
  public HUDManager hudManager;
  private InputAction toggleInventory;
  public MeleeItem activeMelee;
  public RangedItem activeRanged;
  public HealingItem activeHealingItem;
  

  void Awake()
  {
    input = new PlayerControls();
    activeMelee = new MeleeItem();
    activeRanged = new RangedItem();
    activeHealingItem = new HealingItem();
    refreshInventory();
  }

  private void OnEnable()
  {
    //Allowing for input to toggle inventory
    toggleInventory = input.Player.Inventory;
    toggleInventory.Enable();
    toggleInventory.performed += toggleInv;
  }

  private void OnDisable()
  {
    toggleInventory.Disable();
  }


  

  public void toggleInv(InputAction.CallbackContext context)
  {
    if (inventory.activeSelf == isActiveAndEnabled)
    {
      inventory.SetActive(false);
    }
    else
    {
      inventory.SetActive(true);
    }
  }
//Spawning Sprites into their respective locations for melee and ranged weapons and healing items
  public void SetMeleeWeapon(MeleeItem meleeWeapon)
  {
    if (meleeWeaponSlot.GetComponentInChildren<MeleeInventoryItem>() == null)
    {
      SpawnMeleeItem(meleeWeapon,meleeWeaponSlot);
      activeMelee = meleeWeapon;
      refreshInventory();
      hudManager.refreshMeleeHud();
    }
    
  }
  public void SetRangedWeapon(RangedItem rangedWeapon)
  {
    if (meleeWeaponSlot.GetComponentInChildren<GunInventoryItem>() == null)
    {
      SpawnRangedItem(rangedWeapon,rangedWeaponSlot);
      activeRanged = rangedWeapon;
      refreshInventory();
      hudManager.refreshGunHud();

    }
  }
  public void SetHeals(HealingItem heals)
  {
    if (meleeWeaponSlot.GetComponentInChildren<HealsInventoryItem>() == null)
    {
      SpawnHealsItem(heals, healingItemSlot);
      activeHealingItem = heals;
      refreshInventory();

    }
  }
//Do the same for collectible items in the 10 available item slots
  public bool AddItem(Item item)
  {
    for (int i = 0; i < _inventorySlots.Length; i++)
    {
      InventorySlot curSlot = _inventorySlots[i];
      InventoryItem itemInSlot = curSlot.GetComponentInChildren<InventoryItem>();
      if (itemInSlot == null)
      {
        SpawnItem(item,curSlot);
        
        return true;
      }
    }

    return false;
  }

  public void refreshInventory()
  {
    meleeHealthText.text = activeMelee.meleeHealth + "%";
    ammoText.text = activeRanged.GetAmmo() +"";
    healsQuantityText.text = "1";
  }
//Instantitate each item sprite as an InventoryItem
  void SpawnItem(Item item, InventorySlot slot)
  {
    GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
    InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
    inventoryItem.InitializeItem(item);
  }
  void SpawnMeleeItem(MeleeItem item, InventorySlot slot)
  {
    GameObject newItemGameObject = Instantiate(meleeInventoryItemPrefab, slot.transform);
    MeleeInventoryItem inventoryItem = newItemGameObject.GetComponent<MeleeInventoryItem>();
    inventoryItem.InitializeItem(item);
  }
  void SpawnRangedItem(RangedItem item, InventorySlot slot)
  {
    GameObject newItemGameObject = Instantiate(rangeInventoryItemPrefab, slot.transform);
    GunInventoryItem inventoryItem = newItemGameObject.GetComponent<GunInventoryItem>();
    inventoryItem.InitializeItem(item);
  }
  void SpawnHealsItem(HealingItem item, InventorySlot slot)
  {
    GameObject newItemGameObject = Instantiate(healsInventoryItemPrefab, slot.transform);
    HealsInventoryItem inventoryItem = newItemGameObject.GetComponent<HealsInventoryItem>();
    inventoryItem.InitializeItem(item);
  }


 
}
