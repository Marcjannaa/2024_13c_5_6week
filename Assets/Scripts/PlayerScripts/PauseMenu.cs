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
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (GameIsPaused)  Resume();
        else Pause();
    }

    private void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause ()
    {
        print("siemaaaaa");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void OnExit()
    {
        Resume();
    }
    
    public void LoadMainMenu()
    {
        print("whaaaaa");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadUpgradeMenu()
    {
        print("golden keeper shayol wei reporting for duty");
        SceneManager.LoadScene("UpgradeMenu");
    }
}

