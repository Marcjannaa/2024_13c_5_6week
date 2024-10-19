using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Speed;
    private string direction;

    private Rigidbody2D Rb;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Rb.velocity = transform.right * Speed;
    }

    public void setDirection(string direction)
    {
        this.direction = direction;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}