using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaPotion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().enablePlasmaSwimming();
            Destroy(gameObject);
        }
    }
}
