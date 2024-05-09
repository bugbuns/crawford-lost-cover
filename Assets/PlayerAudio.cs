using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip crowbarHit1;
    public AudioClip crowbarHit2;
    public AudioClip crowbarHit3;
    public AudioClip revolverReloadSound;
    public AudioClip shotgunReloadSound;
    public AudioClip shotgunFire;
    public AudioClip crowbarPickup;
    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void pickup()
    {
        if (PlayerStats.Instance.activeMelee.itemType == MeleeItem.MeleeItemType.Crowbar)
        {
            _audioSource.clip = crowbarPickup;
            _audioSource.Play();
        }
    }
    public void fire()
    {
        if (PlayerStats.Instance.activeRanged.itemType == RangedItem.RangedItemType.Shotgun)
        {
            _audioSource.clip = shotgunFire;
            _audioSource.Play();
        }
    }

    public void reload()
    {
        switch (PlayerStats.Instance.activeRanged.itemType)
        {
            case RangedItem.RangedItemType.Revolver:
                _audioSource.clip = revolverReloadSound;
                break;
            case RangedItem.RangedItemType.Shotgun:
                _audioSource.clip = shotgunReloadSound;
                break;
        }
        
        _audioSource.Play();
    }
    public void HitSound()
    {
        if (PlayerStats.Instance.activeMelee.itemType == MeleeItem.MeleeItemType.Crowbar)
        {
            Random rand = new Random();
            int temp = rand.Next(1, 4);
            switch (temp)
            {
                case 1:
                    _audioSource.clip = crowbarHit1;
                    _audioSource.Play();
                    break;
                case 2:
                    _audioSource.clip = crowbarHit2;
                    _audioSource.Play();
                    break;
                case 3:
                    _audioSource.clip = crowbarHit3;
                    _audioSource.Play();
                    break;
            }

        }
    }


}
