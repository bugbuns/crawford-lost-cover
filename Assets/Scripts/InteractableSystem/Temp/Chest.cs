using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        if (Inventory.Instance.hasItem(Item.ItemType.Item1))
        {
            Debug.Log("Opening Chest");
            return true;
        }
        else
        {
            Debug.Log("No Key Found");
            return false;
        }

    }
}
