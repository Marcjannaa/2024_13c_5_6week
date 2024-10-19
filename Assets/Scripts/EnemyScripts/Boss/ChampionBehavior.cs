using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionBehavior : Boss
{
    
    private void Awake()
    {
        MaxHp = 500f;
        Hp = MaxHp;
    }

    protected override IEnumerator Fight()
    {
        yield return null;
    }
}
