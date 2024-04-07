using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    
    //bool
    public bool meleeActive = false;
    public bool gunActive = true;

    //MeleeHUD
    public GameObject meleeHUD;
    public GameObject[] meleeHealthTrackers;
    public Image meleeSprite;
    
    //GunHUD
    public GameObject GunHUD;
    public TextMeshProUGUI bulletCountText;
    public GameObject bulletuiprefab;
    public GameObject bulletHolder;
    public GunSystem _GunSystem;
    public Image gunSprite;
    //Input
    public PlayerControls input;
    private InputAction enableGunHUD;
    private InputAction enableMeleeHUD;
    
    
    
    void Awake()
    {
        GunHUD.SetActive(true);
        if(PlayerStats.Instance.activeRanged!=null)refreshGunHud();
        input = new PlayerControls();
    }
    //Toggle the hud based on what weapon is equipped
    private void toggleGUNHUD(InputAction.CallbackContext context)
    {
        meleeActive = false;
        gunActive = true;
        GunHUD.SetActive(true);
        meleeHUD.SetActive(false);
        if(PlayerStats.Instance.activeRanged!=null)refreshGunHud();

    }
    private void toggleMELEEHUD(InputAction.CallbackContext context)
    {
        meleeActive = true;
        gunActive = false;
        meleeHUD.SetActive(true);
        GunHUD.SetActive(false);
        if(PlayerStats.Instance.activeMelee!=null)refreshMeleeHud();
    }

    public void refreshGunHud()
    {
        bulletCountText.text = "" + _GunSystem.bulletsLeft + "/" +
                               (PlayerStats.Instance.activeRanged.GetAmmo());
        foreach (Transform child in bulletHolder.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < _GunSystem.bulletsLeft; i++)
        {
            GameObject temp = Instantiate(bulletuiprefab, transform);
            temp.transform.parent = bulletHolder.transform;
        }
        gunSprite.sprite = PlayerStats.Instance.activeRanged.GetSprite();

    }

    public void refreshMeleeHud()
    {
        //Set them all inactive first
        for (int i = 0; i < meleeHealthTrackers.Length; i++)
        {
            meleeHealthTrackers[i].SetActive(false);
        }
        
        //Set the proper markers active
        int tempMeleeHealth = PlayerStats.Instance.activeMelee.meleeHealth;

        for (int i = 0; i < tempMeleeHealth; i++)
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
