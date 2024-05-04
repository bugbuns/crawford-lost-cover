using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GunSystem : MonoBehaviour
{
    //Input
    public PlayerControls input;
    private InputAction fireGun;
    private InputAction reload;
    
    //Gun Stats
    public int damage;
    public float timeBetweenShooting, range, reloadTime, timeBetweenShots;
    public int magazineSize;
    public bool allowButtonHold;
    public int bulletsLeft;
    private int bulletsShot;
    
    //bools
    private bool shooting, readyToShoot, reloading;
    
    //Reference
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public HUDManager _HUDManager;

    //Graphics
    public GameObject muzzleFlash;
    private void Awake()
    {
        input = new PlayerControls();
        loadGunSystem();
        readyToShoot = true;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (readyToShoot && !reloading && bulletsLeft>0 && _HUDManager.gunActive)
        {
            Debug.Log("Fire");
            readyToShoot = false;
            
            //RayCast
            if (Physics.Raycast(attackPoint.transform.position, attackPoint.transform.forward, out rayHit, range, whatIsEnemy))
            {
                Debug.Log(rayHit.collider.name);
                if(rayHit.collider.CompareTag("Enemy"))
                    rayHit.collider.GetComponent<EnemyAI>().TakeDamage(damage);
                    rayHit.collider.GetComponent<EnemyAI>().Stun();
            }
            //Graphics
            
            Instantiate(muzzleFlash, attackPoint.transform);
            Invoke("destroyMuzzleFlash",0.75f);
            
            //Decrease Bullets in Gun
            bulletsLeft--;
            _HUDManager.refreshGunHud();
            
            
            Invoke("ResetShot",timeBetweenShooting);
        }
    }

    public void destroyMuzzleFlash()
    {
        Destroy(attackPoint.transform.Find("MuzzleFlash(Clone)").gameObject);
    }

    public void Reload(InputAction.CallbackContext context)
    {
        if (bulletsLeft < magazineSize && !reloading)
        {
            //Reload
            reloading = true;
            Invoke("ReloadFinished",reloadTime);
        }
    }

    private void ReloadFinished()
    {
        Debug.Log("Reloaded");
        int temp=magazineSize - bulletsLeft;
        if (PlayerStats.Instance.activeRanged.GetAmmo() >= temp)
        {
            PlayerStats.Instance.decreaseAmmo(temp);
            bulletsLeft = magazineSize;
        }
        else if (PlayerStats.Instance.activeRanged.GetAmmo() > 0)
        {
            bulletsLeft = PlayerStats.Instance.activeRanged.GetAmmo();
            PlayerStats.Instance.decreaseAmmo(PlayerStats.Instance.activeRanged.GetAmmo());
        }
        _HUDManager.refreshGunHud();
      

       
        
        reloading = false;
    }

    public void loadGunSystem()
    {
        if (PlayerStats.Instance.activeRanged != null)
        {
            magazineSize = PlayerStats.Instance.activeRanged.GetMagazineSize();
            if (PlayerStats.Instance.activeRanged.GetAmmo() >= magazineSize)
            {
                PlayerStats.Instance.decreaseAmmo(magazineSize);
                bulletsLeft = magazineSize;
            }
            else if (PlayerStats.Instance.activeRanged.GetAmmo() > 0)
            {
                bulletsLeft = PlayerStats.Instance.activeRanged.GetAmmo();
                PlayerStats.Instance.decreaseAmmo(PlayerStats.Instance.activeRanged.GetAmmo());
            }

            damage = PlayerStats.Instance.activeRanged.GetDamage();
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void OnEnable()
    {
        //Fire
        if (allowButtonHold) fireGun = input.Player.FireHold;
        else fireGun = input.Player.FireTap;
        fireGun.Enable();
        fireGun.performed += Fire;
        
        //Reload
        reload = input.Player.Reload;
        reload.Enable();
        reload.performed += Reload;


    }

    private void OnDisable()
    {
        fireGun.Disable();
        reload.Disable();
    }
}
