using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void OnExit()
    {
        print("exitclocked");
        Resume();
    }
    
    public void LoadMainMenu()
    {
        print("loadclicked");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadUpgradeMenu()
    {
        print("upgradeclicked");
        SceneManager.LoadScene("UpgradeMenu");
    }
}

