using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

public class Sentry : Enemy, IDamageable
{
    [SerializeField] private bool IsShootingRight;

    [SerializeField] private bool IsShootingLeft;

    [SerializeField] private float _cooldown;

    [SerializeField]  GameObject Projectile;
    
    [SerializeField] private Slider slider;
    private bool _shouldShoot=false;
    private void Start()
    {
        Hp = MaxHp;
        Attack(GameObject.FindGameObjectWithTag("Player"));
    }

    protected override void Attack(GameObject go)
    {
        StartCoroutine(SpawnCoroutine());
    }

    public override void ChangeHp(float damage)
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
            return;
        }
        Hp -= damage;
        UpdateHealthBar();
    }
    private void UpdateHealthBar() { slider.value = Hp / MaxHp; }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            if (IsShootingLeft && _shouldShoot)
            {
                Quaternion leftRotation = Quaternion.Euler(0, 180, 0) * transform.rotation;
                Vector3 pos = transform.position;
                pos.Set(pos.x,pos.y + 0.2f,pos.z);
                Instantiate(Projectile, pos, leftRotation);
            }

            if (IsShootingRight && _shouldShoot)
            {
                Vector3 pos = transform.position;
                pos.Set(pos.x + 1f,pos.y + 0.2f,pos.z);
                Instantiate(Projectile, pos, transform.rotation);
                
            }
            
            yield return new WaitForSeconds(_cooldown);
        }
    }

    private void OnBecameInvisible()
    {
        _shouldShoot = false;
    }

    private void OnBecameVisible()
    {
        _shouldShoot = true;
    }
}
