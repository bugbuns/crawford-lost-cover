using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI _promptText;
    


    private void Start()
    {
        mainCam = Camera.main;
        uiPanel.SetActive(false);
    }

    private void LateUpdate()
    {
        var rotation = mainCam.transform.rotation;
        transform.LookAt(transform.position+rotation*Vector3.forward, rotation*Vector3.up);
    }

    public bool isDisplayed = false;

    public void SetUp(string promptText)
    {
        _promptText.text = promptText;
        uiPanel.SetActive(true);
        isDisplayed = true;

    }

    public void Close()
    {
        uiPanel.SetActive(false);
        isDisplayed = false;
        
    }
}
