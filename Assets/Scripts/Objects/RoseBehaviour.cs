using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RoseBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        other.gameObject.GetComponent<PlayerStash>().Add(PlayerStash.Item.Roses);
        Destroy(gameObject);
    }

}
