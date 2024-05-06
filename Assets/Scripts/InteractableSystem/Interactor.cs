using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
    {
        [Header("Reference Variables")]
        [SerializeField] private Transform interactionPoint;
        [SerializeField] private float interactionPointRadius=0.5f;
        [SerializeField] private LayerMask interactableMask;
        [SerializeField] public InteractionPromptUI _interactionPromptUI;
        
        //Temporary, but necessary for time constraints
        public GameObject door;
        
        //This collider array will hold any objects that the player is allowed to interact with
        private Collider[] colliders = new Collider[3];
        [SerializeField] private int _numFound;

        private IInteractable _interactable;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
        {
            //Add any interactable objects to the collider array
            _numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders,
                interactableMask);
            if (_numFound > 0) //If there are any collliders in range...
            {
               _interactable = colliders[0].GetComponent<IInteractable>();
                if (_interactable != null)
                {
                    if (!_interactionPromptUI.isDisplayed)
                    {
                        _interactionPromptUI.SetUp(_interactable.InteractionPrompt); //Setup the interaction prompt
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (_interactable != door.GetComponent<IInteractable>())
                        {
                            
                            _animator.SetTrigger("isGrabbing");
                        }

                        _interactable.Interact(this); //complete the interaction
                    
                    }
                }
                
            }
            else //If there are no objects in range, close the interaction prompt
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
