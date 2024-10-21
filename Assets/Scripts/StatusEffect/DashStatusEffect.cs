using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashStatusEffect : StatusEffectSO
{

    private PlayerStats _playerStats;
    private float _dashResistance=0.99f;
    private float _damageMultiplier = 0.5f;
    private float _originalResistance;
    private float _originalMeleeDamage;
    private float _originalRangedDamage;

    public DashStatusEffect() //Najlepiej by było mieć to w formie pól, ale w konstruktorze przynajmniej to działa
    {
        statusEffectType=StatusEffectType.Dash;
        activationThreshold=-1f;
        thresholdReductionMultiplier = 0f;
        thresholdReductionEverySecond = 0f;
        activeDuration=2f;
    }

    public override void ApplyEffect(GameObject target)
    {
        base.ApplyEffect(target);
        if (target.CompareTag("Player"))
        {
            _playerStats = target.GetComponent<PlayerStats>();
            _originalResistance = _playerStats.GetCurrentResistance();
            _originalMeleeDamage = _playerStats.GetMeleeDamage();
            _originalRangedDamage = _playerStats.GetRangedDamage();
            _playerStats.SetCurrentResistance(_originalResistance+_dashResistance);
            _playerStats.SetMeleeDamage(_originalMeleeDamage*_damageMultiplier);
            _playerStats.SetRangedDamage(_originalRangedDamage*_damageMultiplier);
        }
    }

    public override void RemoveEffect(GameObject target)
    {
        _playerStats.SetCurrentResistance(_originalResistance);
        _playerStats.SetMeleeDamage(_originalMeleeDamage);
        _playerStats.SetRangedDamage(_originalRangedDamage);
        base.RemoveEffect(target);
    }
}
