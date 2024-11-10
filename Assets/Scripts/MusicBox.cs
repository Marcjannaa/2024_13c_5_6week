using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicBox : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip mainTheme;
    public AudioClip stageTheme;
    public AudioClip championTheme;
    public AudioClip jellyTheme;

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;

        PlayMusicForCurrentScene();
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        PlayMusicForCurrentScene();
    }

    public void PlayMusicForCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            case"Tutorial":
            case"lvl1":
            case"lvl2":
            case"lvl3":
            case"lvl4":
                PlayMusic(stageTheme);
                break;
            default:
                PlayMusic(mainTheme);
                break;
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
    
    public void PlayBossMusic(string bossName)
    {
        switch (bossName)
        {
            case "Jelly":
                PlayMusic(jellyTheme);
                break;
            case "Champion":
                PlayMusic(championTheme);
                break;
        }
    }
}