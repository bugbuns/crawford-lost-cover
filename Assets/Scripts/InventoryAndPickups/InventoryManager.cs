using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
  public InventorySlot[] _inventorySlots;
  public GameObject inventoryItemPrefab;

  private int selectedSlot = -1;

 

  
  public void changeSelectedSlot(int slotNum)
  {
    if (selectedSlot >= 0)
    {
      _inventorySlots[selectedSlot].Deselect();
    }

    _inventorySlots[slotNum].Select();
      selectedSlot = slotNum;
    
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
    inventoryItem.itemSlotNum = curSlotNum;
    inventoryItem.InitializeItem(item);
  }

  public Item getSelectedItem(bool use)
  {
    InventorySlot slot = _inventorySlots[selectedSlot];
    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
    if (itemInSlot != null)
    {
      
      Item item = itemInSlot.item;
      if (use == true)
      {
        Destroy(itemInSlot.gameObject);
      }

      return item;
    }
    else
    {
      return null;
    }
  }
}
