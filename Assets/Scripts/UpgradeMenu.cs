using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private AudioSource aus;
    
    // ilosc dusz, róży
    [SerializeField] private int roseCount, soulsCount;
    [SerializeField] private TMP_Text roseCountTxt, soulsCountTxt;
    
    // koszt ulepszeń
    [SerializeField] private int staminaCost, hpCost, attackCost, shiftCost;
    [SerializeField] private TMP_Text staminaBuff, hpBuff, shiftBuff, attackBuff;
    
    // wartości buffów
    [SerializeField] private float attackVal, hpVal, staminaVal, shiftVal;
    
    // max. parametry jakie aktualnie posiada gracz
    private float _hpCount, _staminaCount, _attackCount, _shiftCount;
    
    public void Start()
    {
        // ilosc dusz, róży
        soulsCount = PlayerPrefs.GetInt("Souls");
        roseCount = PlayerPrefs.GetInt("Roses");
        
        // max. parametry jakie aktualnie posiada gracz
        _hpCount = PlayerPrefs.GetFloat("MaxHp");
        _staminaCount = PlayerPrefs.GetFloat("Stamina");
        _shiftCount = PlayerPrefs.GetFloat("DashDuration");
        _attackCount = PlayerPrefs.GetFloat("MeleeAttack");
        
        // ustawianie tekstu, ilosc roz i soulsow w UI
        roseCountTxt.text = roseCount.ToString();
        soulsCountTxt.text = soulsCount.ToString();
        
        // ustawianie tekstu, ceny i wartosci buffow
        staminaBuff.text = "Stamina will be increased by " + staminaVal + " %" + '\n' + '\n';
        hpBuff.text = "HP will be increased by " + staminaVal + " %" + '\n'+ '\n';
        attackBuff.text = "Attack will be increased by " + staminaVal + " %" + '\n'+ '\n';
        shiftBuff.text = "Shift will be increased by " + staminaVal + " %" + '\n'+ '\n';
        
        staminaBuff.text += "Cost: " + staminaCost + " Dead Roses.";
        shiftBuff.text += "Cost: " + shiftCost + " Dead Roses.";
        hpBuff.text += "Cost: " + hpCost + " Souls.";
        attackBuff.text += "Cost: " + attackCost + " Souls.";
    }

    private void UpdateUI()
    {
        roseCountTxt.text = PlayerPrefs.GetInt("Roses").ToString();
        soulsCountTxt.text = PlayerPrefs.GetInt("Souls").ToString();
    }
    public void OnStaminaUpgrade()
    {
        if (roseCount < staminaCost) return;
        roseCount -= staminaCost;
        PlayerPrefs.SetFloat("Stamina", PlayerPrefs.GetFloat("Stamina") * (1 - (staminaVal / PlayerPrefs.GetFloat("Stamina"))));
        PlayerPrefs.SetInt("Roses", roseCount);
        PlayerPrefs.Save();
        UpdateUI();
        aus.Play();
    }
    public void OnShiftUpgrade()
    {
        if (roseCount < shiftCost) return;
        roseCount -= shiftCost;
        PlayerPrefs.SetFloat("DashDuration", PlayerPrefs.GetFloat("DashDuration") * (1 - (shiftVal / PlayerPrefs.GetFloat("DashDuration"))));
        PlayerPrefs.SetInt("Roses", roseCount);
        PlayerPrefs.Save();
        UpdateUI();
        aus.Play();
    }
    public void OnAttackUpgrade()
    {
        if (soulsCount < attackCost) return;
        soulsCount -= attackCost;
        PlayerPrefs.SetFloat("MeleeAttack", PlayerPrefs.GetFloat("MeleeAttack") * (1 + (attackVal / PlayerPrefs.GetFloat("MeleeAttack"))));
        PlayerPrefs.SetInt("Souls", soulsCount);
        PlayerPrefs.Save();
        UpdateUI();
        aus.Play();
    }
    public void OnHpUpgrade()
    {
        if (soulsCount < hpCost) return;
        soulsCount -= hpCost;
        PlayerPrefs.SetFloat("MaxHp", PlayerPrefs.GetFloat("MaxHp") * (1 + (hpVal / PlayerPrefs.GetFloat("MaxHp"))));
        PlayerPrefs.SetInt("Souls", soulsCount);
        PlayerPrefs.Save();
        UpdateUI();
        aus.Play();
    }

    public void OnExitButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
        PlayerPrefs.GetInt("Paused", 1);
    }

}
