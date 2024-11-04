using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionWave : Projectile
{
    [SerializeField] private float damage = 20f;
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Rb.velocity = transform.right * Speed;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player")){return;}
        other.gameObject.GetComponent<PlayerStats>().DealDamage(damage);
        Destroy(gameObject);
    }
}
