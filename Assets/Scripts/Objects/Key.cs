using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!other.gameObject.CompareTag("Player")) return;
        
        var playerStash = other.gameObject.GetComponent<PlayerStash>();
        playerStash.Add(PlayerStash.Item.Keys);
        Destroy(gameObject);
    }
}
