using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        gameObject.layer = 0;
        StartCoroutine(resetInteractable());
    }

    IEnumerator resetInteractable()
    {
        yield return new WaitForSeconds(42f);
        gameObject.layer = 6;
        hasListened = true;

    }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
