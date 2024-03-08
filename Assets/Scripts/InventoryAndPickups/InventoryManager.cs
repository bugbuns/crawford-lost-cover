using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
  public InventorySlot[] _inventorySlots;
  public GameObject inventoryItemPrefab;
  public GameObject inventory;
  public PlayerControls input;

  private InputAction toggleInventory;
 

  void Awake()
  {
    input = new PlayerControls();
  }

  private void OnEnable()
  {
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

  public bool AddItem(Item item)
  {
    for (int i = 0; i < _inventorySlots.Length; i++)
    {
      InventorySlot curSlot = _inventorySlots[i];
      InventoryItem itemInSlot = curSlot.GetComponentInChildren<InventoryItem>();
      if (itemInSlot == null)
      {
        SpawnItem(item,curSlot, i);
        
        return true;
      }
    }

    return false;
  }

  void SpawnItem(Item item, InventorySlot slot, int curSlotNum)
  {
    GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
    InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
    inventoryItem.InitializeItem(item);
  }

 
}
