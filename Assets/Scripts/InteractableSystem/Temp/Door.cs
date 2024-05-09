using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public GameObject progressBar;
    public QT_Event doorEvent;
    public Animator _animator;

    public string InteractionPrompt => prompt;

    public AudioSource source;
    public AudioClip clip;  
 
    public GameObject reticle;
    public bool doorOpen;
    public bool hasListened;
 
    public bool Interact(Interactor interactor)
    {
        if (!hasListened)
        {
            listen();
            hasListened = true;
            prompt = "Open Door";
            interactor._interactionPromptUI.SetUp(prompt);
            return true;
        }
        
        Debug.Log("Opening Door");
        if (PlayerStats.Instance.activeMelee.itemType == MeleeItem.MeleeItemType.Crowbar)
        {
            reticle.SetActive(false);
            progressBar.SetActive(true);
        }
        else
        {
            prompt = "Door Locked";
        }
        return true;
    }

    private void Update()
    {
        if (doorEvent.eventSuccess&&!doorOpen)
        {
            progressBar.SetActive(false);
            reticle.SetActive(true);
            doorOpen = true;
            openDoor();
        }
    }

    public void openDoor()
    {
        
        //Destroy(gameObject);
        //open door animation
        _animator.SetTrigger("isOpen");
    }

    public void listen()
    {
        //Play Dialogue
        source.PlayOneShot(clip);
 
    }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
