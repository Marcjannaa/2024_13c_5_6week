using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void Start()
    {
        gameObject.tag = "Key";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Destroy(gameObject);
        other.GetComponent<PlayerStash>().Add(PlayerStash.GainedItem.Keys);
    }

    private void OnDestroy()
    {
    }
}
