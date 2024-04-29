using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    
    public void QuitGame(){ 
        Application.Quit();
        Debug.Log("Game has Quit");
    }
}
