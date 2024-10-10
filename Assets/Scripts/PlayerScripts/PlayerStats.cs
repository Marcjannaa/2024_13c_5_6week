using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private TMP_Text txt;
    private float _currHP;
    
    private void Start()
    {
        _currHP = maxHp;
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
