using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField] Transform followTarget;

    [SerializeField] float distanceX = 0.5f;
    [SerializeField] float distanceY = 2.5f;
    [SerializeField] float distanceZ = 0f;

    float rotationY;

    private void Update()
    {
        rotationY += Input.GetAxis("Mouse X");

        transform.position = followTarget.position - Quaternion.Euler(0, rotationY, 0) * new Vector3(0, 0, distanceZ);
    }
}
