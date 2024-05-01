using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    
    public void QuitGame(){
        Application.Quit();
        Debug.Log("Game has Quit");
    }
    
}
