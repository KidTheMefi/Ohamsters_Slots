using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    void OnPreRender()
    {
        Screen.SetResolution(1600, 900, true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);

    }

    public void QuitGame()

    {
        Application.Quit();
    }
}
