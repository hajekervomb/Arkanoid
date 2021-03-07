using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // make methods for: Retry, Quit, Menu

    public void RestartLevel()
    {
        //restart level
        Debug.Log("RESTART LEVEL!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Score = 0;
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
