using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private float jumpForce = 5f;

    private bool _canJump = true;
    private bool _canDoubleJump = false;

    private void OnTriggerStay2D (Collider2D other)
    {
        if (other.CompareTag("Floor"))
        {
            _canJump = true;
            _canDoubleJump = false;
        }
    }

    private void Update() 
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
        }
        
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_canJump){
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                _canJump = false;
                _canDoubleJump = true;
            }
            else if (_canDoubleJump)
            {
                float forceY = (rb.totalForce.y * rb.mass * -1) + jumpForce;
                rb.AddForce(new Vector2(0, forceY), ForceMode2D.Impulse);
                _canDoubleJump = false;
            }
        }
        
    }

    private void FixedUpdate()
    {
        
    }
}
