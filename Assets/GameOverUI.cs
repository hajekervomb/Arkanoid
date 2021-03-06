using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    // make methods for: Retry, Quit, Menu

    public void RestartLevel()
    {
        //restart level
        Debug.Log("RESTART LEVEL!");
    }

    public void QuitGame()
    {
        //quit application
        Application.Quit();
        Debug.Log("QUIT GAME!");
    }

    public void OpenMainMenu()
    {
        //TODO: load main menu scene
        Debug.Log("MAIN MENU OPENED!");
    }
}
