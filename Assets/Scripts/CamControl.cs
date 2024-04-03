using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField] Transform followTarget;

    [SerializeField] private float rotationSpeed = 2f;

    [SerializeField] float distanceX = 0.5f;
    [SerializeField] float distanceY = 2.5f;
    [SerializeField] float distanceZ = 0f;

    [SerializeField] private float minVert = -45;
    [SerializeField] private float maxVert = 45;

    [SerializeField] private Vector2 frameOffset;

    float rotationX;
    float rotationY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * rotationSpeed;
        
        rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
        rotationX += Input.GetAxis("Mouse Y") * rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        var focusPosition = followTarget.position + new Vector3(frameOffset.x, frameOffset.y);

        transform.position = focusPosition - Quaternion.Euler(0, rotationY, 0) * new Vector3(distanceX, distanceY, distanceZ);
        transform.rotation = targetRotation;
    }
    
    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}
