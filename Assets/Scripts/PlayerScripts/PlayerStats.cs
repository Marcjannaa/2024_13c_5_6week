using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private TMP_Text txt;
    [SerializeField] private float defaultMeleeDamage=30;
    [SerializeField] private float defaultRangedDamage=20;
    [SerializeField] private float defaultDashDamage=10;
    [SerializeField] private float _meleeDamage;
    
    private float _currentHp, _rangedDamage, _dashDamage, _currentResistance;
    //private float _currentResistance; percentage of damage avoided (example: 0.15 will reduce incoming damage to 85%)
    
    private void Start()
    {
        _currentHp = maxHp;
        _meleeDamage = defaultMeleeDamage;
        _rangedDamage = defaultRangedDamage;
        _dashDamage = defaultDashDamage;
        UpdateUI();
    }

    public void ChangeHp(float dmg)
    {
        _currentHp -= (1-_currentResistance)*dmg;
        UpdateUI();
    }
    private void UpdateUI()
    {
        txt.SetText("HP:" + _currentHp, false);
    }

    public float GetMeleeDamage()
    {
        return _meleeDamage;
    }
    public float GetRangedDamage()
    {
        return _rangedDamage;
    }
    public float GetDashDamage()
    {
        return _dashDamage;
    }

    public float GetCurrentResistance()
    {
        return _currentResistance;
    }
    

    public void SetMeleeDamage(float damage)
    {
        _meleeDamage = damage;
    }
    public void SetRangedDamage(float damage)
    {
        _rangedDamage = damage;
    }
    public void SetDashDamage(float damage)
    {
        _dashDamage = damage;
    }
    public void SetCurrentResistance(float resistance)
    {
        _currentResistance = resistance;
    }


}
