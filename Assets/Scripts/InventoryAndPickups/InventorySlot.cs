using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
 public Image image;



 public void OnDrop(PointerEventData eventData)
 {
  if (transform.childCount == 0)
  {
   InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
   item.parentAfterDrag = transform;
  }
 }


}
