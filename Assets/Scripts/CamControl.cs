using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float sensitive = 1;
    public Transform target;
    public Transform player;

    private float mouseX;
    private float mouseY;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        ControlCamera();
    }

    void ControlCamera()
    {
        mouseX += Input.GetAxis("Mouse X") * sensitive;
        mouseY += Input.GetAxis("Mouse Y") * sensitive;
        mouseY = Mathf.Clamp(mouseY, -9, 19);
        
        
        transform.LookAt(target);
        
        player.rotation = Quaternion.Euler(0, mouseX, 0);
        
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
}
