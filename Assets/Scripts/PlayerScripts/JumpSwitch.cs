using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSwitch : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        gameObject.GetComponentInParent<PlayerMovement>().ActivateJump();
    }
}
