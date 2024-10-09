using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    // klasa do UI i statów gracza, zrobiłam żeby było cokolwiek, do poprawy później
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private TMP_Text txt;
    private float currHP;
    
    void Start()
    {
        currHP = maxHp;
    }

    public void ChangeHp(float dmg)
    {
        currHP -= dmg;
    }
    void UpdateUI()
    {
        txt.SetText("HP:" + currHP, false);
    }
    void Update()
    {
        UpdateUI();
    }
}
