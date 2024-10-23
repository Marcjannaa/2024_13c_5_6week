using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float MaxHp = 100f;

    protected float Hp;
    
    private void Start()
    {
        Hp = MaxHp;
    }

    protected abstract void Attack(GameObject go);
    public abstract void ChangeHp(float damage);

    private void OnDestroy()
    {
        var objects = FindObjectsOfType<GameObject>();
        foreach (var obj in objects)
            if (obj.CompareTag("Player"))
                obj.GetComponent<PlayerStash>().Add(PlayerStash.Item.Souls);
    }
}
