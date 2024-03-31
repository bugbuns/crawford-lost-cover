using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    
    //
    
    
    
    //MeleeHUD
    public GameObject meleeHUD;
    public GameObject[] meleeHealthTrackers;
    public TextMeshProUGUI meleeHealthText;
    public InventoryManager invManager;
    //GunHUD
    public GameObject GunHUD;
    public TextMeshProUGUI bulletCountText;
    public GameObject bulletuiprefab;
    public GameObject bulletHolder;
    //Input
    public PlayerControls input;
    private InputAction enableGunHUD;
    private InputAction enableMeleeHUD;
    
    
    
    void Awake()
    {
        GunHUD.SetActive(true);
        refreshGunHud();
        input = new PlayerControls();
    }
    //Toggle the hud based on what weapon is equipped
    private void toggleGUNHUD(InputAction.CallbackContext context)
    {
        GunHUD.SetActive(true);
        meleeHUD.SetActive(false);
        refreshGunHud();

    }
    private void toggleMELEEHUD(InputAction.CallbackContext context)
    {
        meleeHUD.SetActive(true);
        GunHUD.SetActive(false);
        refreshMeleeHud();
    }

    public void refreshGunHud()
    {
        //ONLY WORKS FOR PISTOL RIGHT NOW, IMPLEMENT OTHER GUNS LATER
        bulletCountText.text = "" + PlayerStats.Instance.bulletsInGun + "/" +
                           (PlayerStats.Instance.pistolAmmo - PlayerStats.Instance.bulletsInGun);
        foreach (Transform child in bulletHolder.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < PlayerStats.Instance.bulletsInGun; i++)
        {
            GameObject temp = Instantiate(bulletuiprefab, transform);
            temp.transform.parent = bulletHolder.transform;
        }
    }

    public void refreshMeleeHud()
    {
        //Set them all inactive first
        for (int i = 0; i < meleeHealthTrackers.Length; i++)
        {
            meleeHealthTrackers[i].SetActive(false);
        }
        
        //Set the proper markers active
        int tempMeleeHealth = invManager.activeMelee.meleeHealth;
        int numHealthMarkersShown = tempMeleeHealth / 10;

        meleeHealthText.text = tempMeleeHealth + "%";
        
        for (int i = 0; i < numHealthMarkersShown; i++)
        {
            meleeHealthTrackers[i].SetActive(true);
        }
    }
    
   

    private void OnEnable()
    {
        enableMeleeHUD = input.Player.EquipMelee;
        enableGunHUD = input.Player.EquipRanged;
        enableGunHUD.Enable();
        enableMeleeHUD.Enable();
        enableGunHUD.performed += toggleGUNHUD;
        enableMeleeHUD.performed += toggleMELEEHUD;
    }

    private void OnDisable()
    {
        enableGunHUD.Disable();
        enableMeleeHUD.Disable();
    }
}
