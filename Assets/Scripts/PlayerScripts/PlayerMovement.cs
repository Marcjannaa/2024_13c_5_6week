using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private float dashForce = 5f;

    [SerializeField] private float dashCooldown = 10f;

    private bool _canJump = true;
    private bool _canDoubleJump = false;
    private bool _looksToLeft = true;
    private bool _canDash = true;
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
            _looksToLeft = false;
        }
        
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            _looksToLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
           PerformJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PerformDash();
        }
    }

    private void PerformJump()
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

    private void PerformDash()
    {
        if(_canDash){
            float forceX = dashForce * (_looksToLeft ? 1 : -1);
            rb.AddForce(new Vector2(forceX, 0), ForceMode2D.Impulse);
            float dashDmg = GetComponent<PlayerStats>().GetDashDamage();
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(1.5f, 1.5f), 0);
            foreach (var enemy in colliders.Where(e => e.CompareTag("Enemy")))
            {
                enemy.GetComponent<Enemy>().ChangeHp(dashDmg);
            }

            GetComponent<StatusEffectManager>().OnStatusTriggerBuildUp(StatusEffectType.Dash,10f);
            StartCoroutine(DashCooldown());
        }
        
    }

    IEnumerator DashCooldown()
    {
        _canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
    }
    
}
