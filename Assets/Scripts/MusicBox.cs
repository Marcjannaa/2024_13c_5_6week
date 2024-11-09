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

    private void PlayMusicForCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            case"Tutorial":
                PlayMusic(stageTheme);
                break;
            case"lvl1":
                PlayMusic(stageTheme);
                break;
            case"lvl2":
                PlayMusic(stageTheme);
                break;
            case"lvl3":
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
    
    private void PlayBossMusic(string bossName)
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