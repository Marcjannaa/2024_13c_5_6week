using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float maxHp = 100f;

    protected float hp;
    
    private void Start()
    {
        hp = maxHp;
    }

    protected abstract void Attack(GameObject go);
    protected abstract void ChangeHp(float damage);
    protected abstract void LifeSteal();
}
