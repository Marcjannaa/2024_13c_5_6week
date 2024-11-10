using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionBehavior : Boss
{
    [SerializeField] private GameObject championWave;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float championMaxHp = 200;
    [SerializeField] private float meleeWindupDuration = 0.7f;
    [SerializeField] private float meleeAttackDuration = 0.2f;
    [SerializeField] private float meleeAttackRange = 2.5f;
    [SerializeField] private float meleeAttackDistance = 6f;
    [SerializeField] private float meleeAttackDamage = 20f;
    [SerializeField] private float betweenMeleeAttackBreak = 0.1f;
    [SerializeField] private float meleeAttackCooldown = 1f;
    
    [SerializeField] private float dashWindUpDuration = 0.5f;
    [SerializeField] private float dashDamage = 50f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashAttackCoolDown = 1f;
    [SerializeField] private float dashCooldown = 5f;
    [SerializeField] private float dashMaxDistance = 10f;
    [SerializeField] private float dashDuration=0.2f;

    [SerializeField] private float dashRange = 3f;

    private bool _dashReady;
    private StuckDetector _detector;
    private bool _dashHitTaken = false;
    private Animator _animator;
    private void Awake()
    {

        MaxHp = championMaxHp;
        Hp = MaxHp;
        _dashReady = true;
        _detector = GetComponent<StuckDetector>();
        _animator = GetComponent<Animator>();

    }

    private void Update()
    {
        _renderer.flipX = !IsLookingRight();
    }

    protected override IEnumerator Fight()
    {
        musicBox.GetComponent<MusicBox>().PlayBossMusic("Champion");
        yield return new WaitForSeconds(2);
        ShootWave();
        yield return new WaitForSeconds(1);
        while (true)
        {
            if (HorizontalDistanceToPlayer() <= meleeAttackDistance)
            {
                yield return PerformMeleeAttack();
            }
            else if(_dashReady)
            {
                yield return PerformDash();
            }
            else
            {
                ApproachPlayer(meleeAttackDistance,walkSpeed);
            }
            yield return null;
        }
    }

    private void ShootWave()
    {
        _animator.SetTrigger("shoot");
        Vector2 pos = transform.position;
        pos = new Vector2(pos.x + 2f, pos.y-0.5f);
        Quaternion rot = Quaternion.Euler(0, 0, 0) * transform.rotation;
        Instantiate(championWave, pos, rot);
    }
    private IEnumerator ApproachPlayer(float distance,float speed)
    {
        while (HorizontalDistanceToPlayer()>=distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
            _animator.SetBool("walking",true);
            yield return null;
        }
        _animator.SetBool("walking",false);
    }

    private void Attack(float range)
    {
        float posX = range / 2f;
        posX *= IsLookingRight() ? 1 : -1;
        posX += transform.position.x;
        float posY = transform.position.y;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(posX,posY), new Vector2(range, range), 0);
        foreach (var collider in colliders)
        {
            Attack(collider.gameObject);
        }
    }
    private IEnumerator PerformSingleAttack()
    {
        //_renderer.color=Color.red;
        _animator.SetTrigger("attack1");
        yield return new WaitForSeconds(meleeWindupDuration);
        //_renderer.color=Color.magenta;
        Attack(meleeAttackRange);
        yield return new WaitForSeconds(meleeAttackDuration);
        //_renderer.color=Color.white;
    }

    private IEnumerator PerformDoubleAttack()
    {
        yield return PerformSingleAttack();
        //_renderer.color=Color.yellow;
        yield return new WaitForSeconds(betweenMeleeAttackBreak);
        _animator.SetTrigger("attack2");
        yield return PerformSingleAttack();
    }

    private IEnumerator PerformMeleeAttack()
    {
        yield return ApproachPlayer(meleeAttackRange, walkSpeed);
        _currentDamage = meleeAttackDamage;
        int decision = UnityEngine.Random.Range(1, 10);
        if (decision <= 5)
        {
            yield return PerformSingleAttack();
        }
        else
        {
            yield return PerformDoubleAttack();
        }
        //_renderer.color = Color.green;
        yield return new WaitForSeconds(meleeAttackCooldown);
        //_renderer.color = Color.white;
    }

    private IEnumerator PerformDash()
    {
        if (HorizontalDistanceToPlayer() > dashMaxDistance)
        {
            yield return ApproachPlayer(dashMaxDistance, walkSpeed);
        }
        //_renderer.color=Color.cyan;
        _dashHitTaken = false;
        yield return new WaitForSeconds(dashWindUpDuration);
        _animator.SetTrigger("dash run");
        //_renderer.color=Color.red;
        Vector2 endPos = new Vector2(transform.position.x+dashMaxDistance* (IsLookingRight() ? 1 : -1), transform.position.y);
        _detector.Clear();
        _detector.HoldUp();
        while (Math.Abs(endPos.x-transform.position.x)>0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, dashSpeed * Time.deltaTime);
            if(!_dashHitTaken){
                _animator.SetTrigger("dash attack");
                _currentDamage = dashDamage;
                Attack(dashRange);
                _dashHitTaken = true;
            }
            if (_detector.IsStuckX())
            {
                endPos = transform.position;
            }
            yield return null;
        }
        //_renderer.color = Color.green;
        StartCoroutine(CoolDownDash());
        yield return new WaitForSeconds(dashAttackCoolDown);
       // _renderer.color = Color.white;
    }
    private IEnumerator CoolDownDash()
    {
        _dashReady = false;
        yield return new WaitForSeconds(dashCooldown);
        _dashReady = true;
    }
}
