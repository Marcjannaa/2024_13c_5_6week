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
    private void Start()
    {
        if (maxHpWalking != 0f)
        {
            hp = maxHpWalking;
            maxHp = maxHpWalking;
        }
        else
            hp = maxHp;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")){return;}
        var multi = other.gameObject.transform.position.x > transform.position.x ? 1 : -1;
        transform.position += new Vector3(multi * enemySpeed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        GameObject o = other.gameObject;
        if (o.CompareTag("Player"))
            Attack(o);
    }

    protected override void LifeSteal(){}
    
    protected override void ChangeHp(float damage){}
    protected override void Attack(GameObject go)
    {
        go.GetComponent<PlayerStats>().ChangeHp(dmg);
    }
}
