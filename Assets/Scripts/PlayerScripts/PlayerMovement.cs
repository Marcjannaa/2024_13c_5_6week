using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private WorldStateManager _worldManager;

    [SerializeField] private float dashForce = 5f;

    [SerializeField] private float dashCooldown = 10f;
    [SerializeField] private float gaiaDuration = 5f;

    private bool _canJump = true;
    private bool _canDash = true;

    private void OnTriggerStay2D (Collider2D other)
    {
        if (other.CompareTag("Floor"))
        {
            _canJump = true;
            _canDoubleJump = false;
        }
    }

    private bool _canDoubleJump;
    private bool _looksToLeft;


    private void Update() 
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
            _looksToLeft = true;

        }
        
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            _looksToLeft = false;
        }
        
        
        if (Input.GetMouseButtonDown(0))     
            Attack();

        
        if (Input.GetKeyDown(KeyCode.Space))
           PerformJump();
        

        if (Input.GetKeyDown(KeyCode.LeftShift))
            PerformDash();
        

    }

    private void PerformJump()
    {
        if(_canJump){
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            _canJump = false;
            _canDoubleJump = true;
        }
        else if (_canDoubleJump) //TODO: poprawić przypisywanie siły żeby nie robić "efektu domina" i móc wyskakiwać z dziur w trakcie spadania
        {
            float forceY = (rb.totalForce.y * rb.mass * -1) + jumpForce;
            rb.AddForce(new Vector2(0, forceY), ForceMode2D.Impulse);
            _canDoubleJump = false;
        }
    }

    private void PerformDash()
    {
        if(_canDash){
            float forceX = dashForce * (_looksToLeft ? -1 : 1);
            rb.AddForce(new Vector2(forceX, 0), ForceMode2D.Impulse);
            float dashDmg = GetComponent<PlayerStats>().GetDashDamage();
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(1.5f, 1.5f), 0);
            foreach (var enemy in colliders.Where(e => e.CompareTag("Enemy"))) //BUG: enemies take no damage
            {
                enemy.GetComponent<Enemy>().ChangeHp(dashDmg);
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, 1), ForceMode2D.Impulse);
            }

            //GetComponent<StatusEffectManager>().OnStatusTriggerBuildUp(StatusEffectType.Dash,10f);
            StartCoroutine(DashCooldown());
            _worldManager.ChangeState();
            StartCoroutine(GaiaDuration());
        }
    }

    IEnumerator DashCooldown()
    {
        _canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
    }

    
    IEnumerator GaiaDuration()
    {
        yield return new WaitForSeconds(gaiaDuration);
        _worldManager.ChangeState();
    }
    


    private void Attack()
    {
        var hitInfo = Physics2D.Raycast(
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), 
            (_looksToLeft ? transform.right * -1 : transform.right), 
            0.1f
            );
        Debug.DrawRay(
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), 
             _looksToLeft ? transform.right * -2f : transform.right * 2f, 
                 Color.cyan
             );
        if (hitInfo.collider.gameObject.CompareTag("Enemy") && hitInfo.collider != null)
        {
            var dmg = gameObject.GetComponent<PlayerStats>().GetMeleeDamage();
            hitInfo.collider.gameObject.GetComponent<WalkingEnemy>().ChangeHp(dmg);
            print("hit");
        }
    }

    public void ActivateJump()
    {
        _canJump = true;
        _canDoubleJump = false;
    }

}
