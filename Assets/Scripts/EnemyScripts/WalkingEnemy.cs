using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    [SerializeField] private float maxHpWalking;
    [SerializeField] private float enemySpeed = 2f;
    [SerializeField] private float dmg = 5f;
    [SerializeField] private AudioSource aus;
    private const float DmgCooldown = 0.2f;
    private float _counter = 0f;
    private SpriteRenderer _renderer;
    
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (maxHpWalking != 0f)
        {
            Hp = maxHpWalking;
            MaxHp = maxHpWalking;
        }
        else Hp = MaxHp;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) { return; }
        var multi = other.gameObject.transform.position.x > transform.position.x ? 1 : -1;
        transform.position += new Vector3(multi * enemySpeed * Time.deltaTime, 0, 0);
        _renderer.flipX = multi < 0;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")){return;}
        if (_counter <= 0f)
        {
            Attack(other.gameObject);
            _counter = DmgCooldown;
        }else _counter -= Time.deltaTime;
    }
    

    public override void ChangeHp(float damage)
    {
        if (Hp - damage <= 0)
        {
            aus.Play();
            aus.volume = PlayerCollisions.volume;
            Destroy(gameObject);
            return;
        }
        Hp -= damage;
        gameObject.GetComponent<HpBar>().UpdateBar(Hp, maxHpWalking);
    }
    protected override void Attack(GameObject go)
    {
        go.GetComponent<PlayerStats>().DealDamage(dmg);
    }


}
