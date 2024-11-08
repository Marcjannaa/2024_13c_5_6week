using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool newGame;
    public void NewGame()
    {
        newGame = true;
        SceneManager.LoadScene("LoadGame");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadGame()
    {
        newGame = false;
        SceneManager.LoadScene("LoadGame");
    }

    public void Exit()
    {
        print("Quitting..");
    }
    
}
