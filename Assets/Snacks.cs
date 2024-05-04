using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snacks : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;
    public HUDManager _HUDManager;

    public bool Interact(Interactor interactor)
    {
        Eat();
        return true;

    }

    public void Eat()
    {
        PlayerStats.Instance.health += 10;
        _HUDManager.setHealthBar();
        Destroy(gameObject);

    }

}
