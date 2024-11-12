using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoseBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource aus;
    [SerializeField] private AudioClip[] aucs;
    private AudioClip auc;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        int index = Random.Range(0, aucs.Length);
        
        auc = aucs[index];
        aus.clip = auc;
        aus.Play();
        
        other.gameObject.GetComponent<PlayerStash>().Add(PlayerStash.Item.Roses);
        Destroy(gameObject);
    }

}
