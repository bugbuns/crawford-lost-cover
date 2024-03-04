using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmmoPickup : MonoBehaviour
{
    public int ammoCount;
    
    void Awake()
    {
        ammoCount = Random.Range(1, 11);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the player collides with the object, increase the player's ammo
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            
            other.GetComponent<MockController>()._PlayerStats.ammo += ammoCount;
            Debug.Log(other.GetComponent<MockController>()._PlayerStats.ammo);
            Destroy(this.gameObject);
        }
    }
}
