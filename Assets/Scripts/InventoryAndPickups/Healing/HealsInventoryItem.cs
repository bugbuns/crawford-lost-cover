using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HealsInventoryItem : MonoBehaviour
{
  public HealingItem item;
  
  
  public Image image;

  
  public void InitializeItem(HealingItem newItem)
  {
    item = newItem;
    image.sprite = newItem.GetSprite();
  }

  

  
}
