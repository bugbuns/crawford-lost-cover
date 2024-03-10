using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GunInventoryItem : MonoBehaviour
{
  public RangedItem item;
  
  
  public Image image;


  public void InitializeItem(RangedItem newItem)
  {
    item = newItem;
    image.sprite = newItem.GetSprite();
  }

  


}
