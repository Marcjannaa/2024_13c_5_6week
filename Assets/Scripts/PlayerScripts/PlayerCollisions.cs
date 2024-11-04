using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    private Vector2 checkedPosition;

    private void Start()
    {
        checkedPosition = transform.position;
    }
    public void Die()
    {
        transform.position = checkedPosition;
        gameObject.GetComponent<PlayerStats>().SetHp(0);
        gameObject.GetComponent<PlayerStats>().UpdateUI();
        //gameObject.GetComponent<PlayerStats>().ResetStats();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            checkedPosition = transform.position;
            PlayerPrefs.SetString("LastScene" + LoadMenuHandler.slotNum, SceneManager.GetActiveScene().name);
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
            Die();
            gameObject.GetComponent<PlayerStats>().UpdateUI();
        }
    }
}
