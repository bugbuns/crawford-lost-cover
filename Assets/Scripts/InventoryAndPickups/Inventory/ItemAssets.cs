using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
   public static ItemAssets Instance { get; private set; }

   private void Awake()
   {
      Instance = this;
   }



   public Sprite Item1Sprite;
   public Sprite Item2Sprite;
   public Sprite Item3Sprite;
}
