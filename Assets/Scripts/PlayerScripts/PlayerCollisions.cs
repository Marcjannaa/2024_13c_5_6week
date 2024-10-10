using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private Vector2 checkedPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            checkedPosition = transform.position;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("FallZone"))
            transform.position = checkedPosition;
    }
}
