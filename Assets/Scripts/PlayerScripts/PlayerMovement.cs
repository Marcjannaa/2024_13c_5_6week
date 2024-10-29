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

    private float dir;

    private bool _canDoubleJump;
    private bool _looksToLeft;
    private const float _attackCooldownCount = 0.3f;
    private bool _attackCooldown = true;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A)) _looksToLeft = true;
        else if (Input.GetKey(KeyCode.D)) _looksToLeft = false;

        if (Input.GetAxis("Fire1") > 0 && _attackCooldown)
        {
            gameObject.GetComponentInChildren<ColorMode>().SetPlayerState(ColorMode.PlayerState.Idle);
            gameObject.GetComponentInChildren<ColorMode>().UpdateColor();

            _attackCooldown = false;
            gameObject.GetComponent<PlayerMeleeAttack>().DealDamage();
            StartCoroutine(AttackCooldown());
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
           PerformJump();
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
            PerformDash();
        gameObject.GetComponentInChildren<PlayerSprites>().LookLeft(_looksToLeft);
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
    private IEnumerator AttackCooldown()
    {
        _attackCooldown = false;
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("Stamina"));
        gameObject.GetComponentInChildren<ColorMode>().SetPlayerState(ColorMode.PlayerState.Idle);
        gameObject.GetComponentInChildren<ColorMode>().UpdateColor();
        _attackCooldown = true;
    }

    IEnumerator DashCooldown()
    {
        _canDash = false;
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("DashDuration"));
        _canDash = true;
    }
    
    IEnumerator GaiaDuration()
    {
        yield return new WaitForSeconds(gaiaDuration);
        _worldManager.ChangeState();
    }

    public void ActivateJump()
    {
        _canJump = true;
        _canDoubleJump = false;
    }

    private void FixedUpdate()
    {
        dir = Input.GetAxis("Horizontal");
        transform.position += new Vector3(dir * moveSpeed * Time.deltaTime, 0, 0);
    }
}
