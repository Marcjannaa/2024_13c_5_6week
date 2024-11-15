using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    private Vector2 checkedPosition;
    public static float volume;
    [SerializeField] private AudioClip rosePickUpClip, checkPointPickUpClip;
    [SerializeField] private AudioSource aus;
    private void Start()
    {
        checkedPosition = transform.position;
    }

    private void Die()
    {
        transform.position = checkedPosition;
        gameObject.GetComponent<PlayerStats>().SetHp(0);
        gameObject.GetComponent<PlayerStats>().UpdateUI();
    }

    public static void ChangeVolume(float volume) { PlayerCollisions.volume = volume; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            checkedPosition = transform.position;
            aus.clip = checkPointPickUpClip;
            aus.volume = volume;
            aus.Play();
            //PlayerPrefs.SetString("LastScene" + LoadMenuHandler.slotNum, SceneManager.GetActiveScene().name);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("FallZone"))
        {
            gameObject.GetComponent<PlayerStats>().UpdateUI();
            transform.position = checkedPosition;
            Die();
        }
        else if (other.gameObject.CompareTag("Projectile"))
        {
            //dostan dmg na morde
            Destroy(other.gameObject); 
            gameObject.GetComponent<PlayerStats>().UpdateUI();

        }
        else if (other.gameObject.CompareTag("Plasma"))
        {
            if (gameObject.GetComponent<PlayerStats>().getPlasmaStatus()) return;
            Die();
            gameObject.GetComponent<PlayerStats>().UpdateUI();
        }else if (other.gameObject.CompareTag("Rose"))
        {
            aus.clip = rosePickUpClip;
            aus.volume = volume;
            aus.Play();
        }
    }
}
