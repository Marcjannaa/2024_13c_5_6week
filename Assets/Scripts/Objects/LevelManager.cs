using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string _sceneName = "Level";
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        PlayerPrefs.SetString("LastScene", _sceneName);
        Destroy(other.gameObject);
        SceneManager.LoadScene(_sceneName);
    }
}
