using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Tutorial");
        PlayerPrefs.SetInt("Roses", 0);
        PlayerPrefs.SetInt("Souls", 0);
        PlayerPrefs.SetFloat("MeleeAttack", 10f);
        PlayerPrefs.SetFloat("MaxHp", 100f);
        PlayerPrefs.SetFloat("DashDuration", 1f);
        PlayerPrefs.SetFloat("Stamina", 0.3f);
        PlayerPrefs.Save();
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
