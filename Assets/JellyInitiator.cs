using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JellyInitiator : MonoBehaviour
{
    [SerializeField] private GameObject jelly;
    private void OnTriggerEnter2D(Collider2D other)
    {

            print("jelly activated");
            jelly.SetActive(true);
            Destroy(gameObject);
    }
}
