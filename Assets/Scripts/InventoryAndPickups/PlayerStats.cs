using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStats
{
    public static PlayerStats Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public int health=100;
    public int pistolAmmo=0;
    public int shotgunAmmo = 0;
    
    

}
