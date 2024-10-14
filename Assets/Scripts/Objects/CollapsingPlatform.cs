using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    [SerializeField] private float DestroyDelay;
    [SerializeField] private float RespawnCooldown;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DestroyAfterDelay());
        }
    }
    
    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(DestroyDelay);
        
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        
        yield return new WaitForSeconds(RespawnCooldown);
        
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
    }
}
