using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadControlMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
