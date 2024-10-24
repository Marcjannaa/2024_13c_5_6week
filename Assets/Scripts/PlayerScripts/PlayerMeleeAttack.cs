using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float attackRange = 0.3f;
    private bool _shouldDamage;
    private RaycastHit2D[] hits;
    Vector2 t;
    private string key;
    
    public void DealDamage()
    {
        hits = Physics2D.CircleCastAll(transform.position, attackRange, transform.right, 0f, attackableLayer);
        if (Input.GetAxis("Vertical") > 0)
        {
            hits = Physics2D.CircleCastAll(transform.position, attackRange, transform.up * Input.GetAxis("Vertical"), 1f, attackableLayer);
        }else if (Input.GetAxis("Horizontal") > 0)
        {
            hits = Physics2D.CircleCastAll(transform.position, attackRange, transform.right * Input.GetAxis("Horizontal"), 1f, attackableLayer);
        }
        
        foreach (var t in hits)
        {
            var idamageable = t.collider.gameObject.GetComponent<IDamageable>();
            
            print(t.collider.gameObject.name);
            print(t.collider.gameObject.layer);
            print(t.collider.gameObject.tag);
            
            if (idamageable == null) continue;
            
            idamageable.Damage(
                gameObject.GetComponent<PlayerStats>().GetMeleeDamage(), t.collider.gameObject
            );
            print("hiiiit1!!");
        }
    }
}
