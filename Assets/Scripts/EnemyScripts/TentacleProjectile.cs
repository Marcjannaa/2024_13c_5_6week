using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleProjectile : MonoBehaviour
{
    [SerializeField] protected float Speed;


    protected Rigidbody2D Rb;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }
    
    public void Initialize(GameObject targetPlayer)
    {
        Vector2 direction = (targetPlayer.transform.position - transform.position).normalized;
        Rb.velocity = direction * Speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().DealDamage(20);
        }
    }
}
