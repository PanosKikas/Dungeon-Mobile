using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject OptionsMenu;

    void Start()
    {
        MainMenuButton();
    }

    public void PlayNowButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Test Chamber");
    }

    public void OptionsButton()
    {
        if (MainMenu.activeSelf)
        {
            MainMenu.SetActive(false); 
        }

        if (!OptionsMenu.activeSelf)
        {
            OptionsMenu.SetActive(true); 
        }
    }

    public void MainMenuButton()
    {
        if (!MainMenu.activeSelf)
        {
            MainMenu.SetActive(true); 
        }

        if (OptionsMenu.activeSelf)
        {
            OptionsMenu.SetActive(false); 
        }
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}