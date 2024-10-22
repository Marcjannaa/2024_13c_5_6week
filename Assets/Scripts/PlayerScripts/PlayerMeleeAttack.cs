using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float attackRange = 5f;
    private bool _shouldDamage;
    private RaycastHit2D[] hits;
    
    public void DealDamage()
    {
        hits = Physics2D.CircleCastAll(transform.position, attackRange, transform.right, 0f, attackableLayer);
        foreach (var t in hits)
        {
            IDamageable idamageable = t.collider.gameObject.GetComponent<IDamageable>();
            if (idamageable != null)
            {
                idamageable.Damage(
                    gameObject.GetComponent<PlayerStats>().GetMeleeDamage(), t.collider.gameObject
                    );
                print("hiiiit1!!");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_transform.position, attackRange);
    }
}
