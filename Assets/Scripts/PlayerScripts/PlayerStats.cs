using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float defaultMeleeDamage=30;
    [SerializeField] private float defaultRangedDamage=20;
    [SerializeField] private float defaultDashDamage=10;
    [SerializeField] private float meleeDamage;
    [SerializeField] private TMP_Text soulsTxt, rosesTxt, keysTxt, hpTxt;
    private float _currentHp, _rangedDamage, _dashDamage, _currentResistance;
    //private float _currentResistance; percentage of damage avoided (example: 0.15 will reduce incoming damage to 85%)
    
    private void Start()
    {
        _currentHp = maxHp;
        meleeDamage = defaultMeleeDamage;
        _rangedDamage = defaultRangedDamage;
        _dashDamage = defaultDashDamage;
        UpdateUI();
    }

    public void ResetStats()
    {
        _currentHp = maxHp;
    }
    public void ChangeHp(float dmg)
    {
        _currentHp -= (1-_currentResistance)*dmg;
        UpdateUI();
        if (_currentHp <= 0)
        {
            gameObject.GetComponent<PlayerCollisions>().Die();
        }
    }
    public void UpdateUI()
    {
        rosesTxt.SetText("roses: " + GetComponent<PlayerStash>().GetRoses());
        keysTxt.SetText("keys: " + GetComponent<PlayerStash>().GetKeys());
        soulsTxt.SetText("souls: " + GetComponent<PlayerStash>().GetSouls());
        hpTxt.SetText("HP: " + _currentHp);
    }

    public float GetMeleeDamage()
    {
        return meleeDamage;
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
        meleeDamage = damage;
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
