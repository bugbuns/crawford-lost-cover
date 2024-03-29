using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius=0.5f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;
    

    private Collider[] colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private IInteractable _interactable;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders,
            interactableMask);
        if (_numFound > 0)
        {
           _interactable = colliders[0].GetComponent<IInteractable>();
            if (_interactable != null)
            {
                if (!_interactionPromptUI.isDisplayed)
                {
                    _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                }

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    _interactable.Interact(this);
                }
            }
            
        }
        else
        {
            if(_interactable!=null){_interactable = null;}
            if(_interactionPromptUI.isDisplayed) {_interactionPromptUI.Close(); }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position,interactionPointRadius);
    }
}
