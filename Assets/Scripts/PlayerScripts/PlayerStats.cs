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
    private float _currHP;
    private float _meleeDamage;
    private float _rangedDamage;
    private float _dashDamage;
    
    private void Start()
    {
        _currHP = maxHp;
        _meleeDamage = defaultMeleeDamage;
        _rangedDamage = defaultRangedDamage;
        _dashDamage = defaultDashDamage;
    }

    public void ChangeHp(float dmg)
    {
        _currHP -= dmg;
        UpdateUI();
    }
    private void UpdateUI()
    {
        txt.SetText("HP:" + _currHP, false);
    }
}
