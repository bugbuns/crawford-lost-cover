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

   [Header("Melee")] 
   public Sprite Fists;
   public Sprite Crowbar;
   public Sprite Bat;

   [Header("Guns")] 
   public Sprite Pistol;
   public Sprite Shotgun;
   public Sprite TommyGun;

   [Header("Heals")] 
   public Sprite Medkit;
   
   
   [Header("Collectibles")]
   public Sprite Item1Sprite;
   public Sprite Item2Sprite;
   public Sprite Item3Sprite;
}
