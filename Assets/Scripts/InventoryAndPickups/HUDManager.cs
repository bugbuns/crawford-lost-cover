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
    [HideInInspector] public bool meleeActive = true;
    [HideInInspector] public bool gunActive = false;
    [HideInInspector] public bool healsActive = false;

    //MeleeHUD
    [Header("MeleeHUD")]
    public GameObject meleeHUD;
    public Image MeleeHUDPlayerHealth;
    public GameObject[] meleeHealthTrackers;
    public Image meleeSprite;
    
    [Header("GunHUD")]
    public GameObject GunHUD;
    public Image GunHUDPlayerHealth;
    public TextMeshProUGUI bulletCountText;
    public GameObject bulletuiprefab;
    public GameObject bulletHolder;
    public GunSystem _GunSystem;
    public Image gunSprite;

    [Header("HealingHUD")] 
    public HealingSystem healingSystem;
    public GameObject healingHUD;
    public Image HealingHUDPlayerHealth;
    public Image healsSprite;
    public TextMeshProUGUI healsQuantity;
    //Input
    
    public PlayerControls input;
    private InputAction enableGunHUD;
    private InputAction enableMeleeHUD;
    private InputAction enableHealHUD;
    
    
    
    void Awake()
    {
        if(PlayerStats.Instance.activeRanged!=null)refreshGunHud();
        meleeActive = true;
        gunActive = false;
        input = new PlayerControls();
    }

    private void Start()
    {
        meleeHUD.SetActive(true);
        meleeSprite.sprite = PlayerStats.Instance.activeMelee.GetSprite();
        setHealthBar();
    }

    //Toggle the hud based on what weapon is equipped
    private void toggleGUNHUD(InputAction.CallbackContext context)
    {
        if (healingSystem.isHealing) healingSystem.healCancelled = true;
        meleeActive = false;
        gunActive = true;
        meleeHUD.SetActive(false);
        GunHUD.SetActive(true);
        healsActive = false;
        healingHUD.SetActive(false);
        setHealthBar();
        if(PlayerStats.Instance.activeRanged!=null)refreshGunHud(); 
    }
    private void toggleMELEEHUD(InputAction.CallbackContext context)
    {
        if (healingSystem.isHealing) healingSystem.healCancelled = true;
        meleeActive = true;
        gunActive = false;
        meleeHUD.SetActive(true);
        GunHUD.SetActive(false);
        healsActive = false;
        healingHUD.SetActive(false);
        
        setHealthBar();
        if(PlayerStats.Instance.activeMelee!=null)refreshMeleeHud(); 
    }

    private void toggleHEALINGHUD(InputAction.CallbackContext context)
    {
        meleeActive = false;
        gunActive = false;
        meleeHUD.SetActive(false);
        GunHUD.SetActive(false);
        healsActive = true;
        healingHUD.SetActive(true);
        setHealthBar();
        if(PlayerStats.Instance.ActiveHealingItem!=null)refreshHealingHud(); 
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
        //Set Sprite
        meleeSprite.sprite=PlayerStats.Instance.activeMelee.GetSprite();
        meleeSprite.color=Color.white;

        for (int i = 0; i < tempMeleeHealth; i++)
        {
            meleeHealthTrackers[i].SetActive(true);
        }
    }

    public void refreshHealingHud()
    {
        healsSprite.sprite = PlayerStats.Instance.ActiveHealingItem.GetSprite();
        healsQuantity.text = PlayerStats.Instance.ActiveHealingItem.quantity+"x";
        setHealthBar();
    }

    public void setHealthBar()
    {
        GunHUDPlayerHealth.fillAmount = PlayerStats.Instance.health/100f; 
        MeleeHUDPlayerHealth.fillAmount = PlayerStats.Instance.health/100f;
        HealingHUDPlayerHealth.fillAmount = PlayerStats.Instance.health / 100f;
    }
    
   

    private void OnEnable()
    {
        enableMeleeHUD = input.Player.EquipMelee;
        enableGunHUD = input.Player.EquipRanged;
        enableHealHUD = input.Player.EquipHeals;
        enableGunHUD.Enable();
        enableMeleeHUD.Enable();
        enableHealHUD.Enable();

        enableGunHUD.performed += toggleGUNHUD;
        enableMeleeHUD.performed += toggleMELEEHUD;
        enableHealHUD.performed += toggleHEALINGHUD;
    }

    private void OnDisable()
    {
        enableGunHUD.Disable();
        enableMeleeHUD.Disable();
    }
}
