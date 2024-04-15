using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Opening Door");
        if (PlayerStats.Instance.activeMelee.itemType == MeleeItem.MeleeItemType.Crowbar)
        {
            openDoor();
        }
        else
        {
            prompt = "Door Locked";
        }
        return true;
    }

    public void openDoor()
    {
        Destroy(gameObject);
    }
}
