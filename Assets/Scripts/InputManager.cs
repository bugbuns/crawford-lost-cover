using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls pControl;

    public Vector2 movementInput;
    public float vertInput;
    public float horiInput;


    private void OnEnable()
    {
        if (pControl == null)
        {
            pControl = new PlayerControls();

            pControl.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        pControl.Enable();
    }

    private void OnDisable()
    {
        pControl.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        vertInput = movementInput.y;
        horiInput = movementInput.x;
    }
}
