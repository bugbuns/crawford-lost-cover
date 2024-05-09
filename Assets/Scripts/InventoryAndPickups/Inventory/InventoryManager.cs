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
  public GunSystem gunSystem;
  public playerController _PlayerController;
  public PlayerAudio _PlayerAudio;
  private InputAction toggleInventory;
  



  void Awake()
  {
    input = new PlayerControls();
    
    
    //Start with fists as weapon
    MeleeItem fistsStart = new MeleeItem();
    fistsStart.itemType = MeleeItem.MeleeItemType.Fists;
    PlayerStats.Instance.activeMelee = fistsStart;
    
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
    
    refreshInventory();
    if (inventory.activeSelf == isActiveAndEnabled)
    {
      inventory.SetActive(false);
      
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
    }
    else
    {
      inventory.SetActive(true);
      
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
    }
    
  }
//Spawning Sprites into their respective locations for melee and ranged weapons and healing items
  public void SetMeleeWeapon(MeleeItem meleeWeapon)
  {
    if (meleeWeaponSlot.GetComponentInChildren<MeleeInventoryItem>() != null)
    {
      Destroy(meleeWeaponSlot.transform.GetChild(0).gameObject);
    }
    SpawnMeleeItem(meleeWeapon,meleeWeaponSlot);
    PlayerStats.Instance.activeMelee = meleeWeapon;
    _PlayerAudio.pickup();
    _PlayerController.weaponEquipped = true;
    hudManager.refreshMeleeHud();
    
  }
  public void SetRangedWeapon(RangedItem rangedWeapon)
  {
    if (rangedWeaponSlot.GetComponentInChildren<GunInventoryItem>() != null)
    {
      Destroy(rangedWeaponSlot.transform.GetChild(0).gameObject);
    }
    SpawnRangedItem(rangedWeapon,rangedWeaponSlot);
    PlayerStats.Instance.activeRanged = rangedWeapon;
    gunSystem.loadGunSystem();
    hudManager.refreshGunHud();
  }
  public void SetHeals(HealingItem heals)
  {
    if (healingItemSlot.GetComponentInChildren<HealsInventoryItem>()==null)
    {
      SpawnHealsItem(heals, healingItemSlot);
      PlayerStats.Instance.ActiveHealingItem = heals;
      hudManager.refreshHealingHud();

    }
    else if (healingItemSlot.GetComponentInChildren<HealsInventoryItem>().item.itemType == heals.itemType)
    {
      //Stack
      PlayerStats.Instance.ActiveHealingItem.quantity++;
    }
    else
    {
      removeHealingItem();
      SpawnHealsItem(heals, healingItemSlot);
      PlayerStats.Instance.ActiveHealingItem = heals;
      hudManager.refreshHealingHud();
    }
  }

  public void removeHealingItem()
  {
    Destroy(healingItemSlot.transform.GetChild(0).gameObject);
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
    Debug.Log(PlayerStats.Instance.activeMelee);
    if (PlayerStats.Instance.activeMelee.itemType!= null&&PlayerStats.Instance.activeMelee.itemType!=MeleeItem.MeleeItemType.Fists)
    {
      meleeHealthText.text = PlayerStats.Instance.activeMelee.meleeHealth+"";
    }

    if (PlayerStats.Instance.activeRanged != null)
    {
      
      ammoText.text = gunSystem.bulletsLeft+"/" + PlayerStats.Instance.activeRanged.GetAmmo();
    }
    if(PlayerStats.Instance.ActiveHealingItem!=null)healsQuantityText.text =
      PlayerStats.Instance.ActiveHealingItem.quantity+ "";
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
