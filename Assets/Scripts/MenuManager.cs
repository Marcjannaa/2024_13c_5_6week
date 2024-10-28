using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("LoadGame");
    }

    public void Exit()
    {
        print("Quitting..");
    }
    
}
