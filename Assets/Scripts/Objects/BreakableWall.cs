using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : Enemy
{
    protected override void Attack(GameObject go)
    {
    }

    public override void ChangeHp(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected override void LifeSteal()
    {
    }
}
