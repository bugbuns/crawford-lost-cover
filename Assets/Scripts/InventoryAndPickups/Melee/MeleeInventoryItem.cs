using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MeleeInventoryItem : MonoBehaviour
{
  public MeleeItem item;
  public Image image;
  
  
  
  public void InitializeItem(MeleeItem newItem)
  {
    item = newItem;
    image.sprite = newItem.GetSprite();
  }




}
