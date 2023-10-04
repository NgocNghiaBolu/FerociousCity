using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool continueGame = false;
    public bool startGame = false;

    public static MainMenu instance;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        instance = this;
    }

    public void LoadContinue()
    {
        continueGame = true;
        SceneManager.LoadScene("Town");
    }

    public void StartGame()
    {
        startGame = true;
        SceneManager.LoadScene("Town");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
