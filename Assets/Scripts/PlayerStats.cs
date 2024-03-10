using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStats:MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        

    }
    public int health;
    public int pistolAmmo;
    public int shotgunAmmo;
    public int TommyGunAmmo;



}
