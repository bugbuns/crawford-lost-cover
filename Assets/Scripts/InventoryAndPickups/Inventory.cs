using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory:MonoBehaviour
{
   public static Inventory Instance { get; private set; }

   private List<Item> itemList;

   public void Awake()
   {
      Instance = this;
   }

   public Inventory()
   {
      itemList = new List<Item>();
   }

   public void AddItem(Item item)
   {
      itemList.Add(item);
      
   }
}
