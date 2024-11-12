using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private float defaultMeleeDamage=5f;
    [SerializeField] private float defaultRangedDamage=20;
    [SerializeField] private float defaultDashDamage=10;
    [SerializeField] private float meleeDamage;
    [SerializeField] private TMP_Text soulsTxt, rosesTxt, keysTxt;
    private float _currentHp, _rangedDamage, _dashDamage, _currentResistance;
    [SerializeField] private Image heart;
    private int _heartCount;
    [SerializeField] private List<RawImage> hearts;
    [SerializeField] private Slider slider;
    private bool canSwimInPlasma;

    //private float _currentResistance; percentage of damage avoided (example: 0.15 will reduce incoming damage to 85%)
    
    private void Start()
    {
        canSwimInPlasma = false;
        _heartCount = hearts.Count;
        _currentHp = PlayerPrefs.GetFloat("MaxHp");//_currentHp = maxHp;
        meleeDamage = PlayerPrefs.GetFloat("MeleeAttack");//meleeDamage = defaultMeleeDamage;
        canSwimInPlasma = PlayerPrefs.GetInt("CanSwim") == 1;
        _rangedDamage = defaultRangedDamage;
        _dashDamage = defaultDashDamage;

        RefreshHearts();
        UpdateUI();
    }

    private void RefreshHearts()
    {
        for (var i = hearts.Count - 1; i >= 0; i--)
        {
            hearts[i].GetComponent<Heart>().ChangeHeartCount(i < _heartCount);
            gameObject.GetComponentInChildren<ColorMode>().UpdateColor();
        }
    }
    private void ResetHp()
    {
        _currentHp = PlayerPrefs.GetFloat("MaxHp");//_currentHp = maxHp;
    }

    public void DealDamage(float dmg)
    {
        _currentHp -= (1-_currentResistance)*dmg;
        UpdateUI();
    }
    public void UpdateUI()
    {
        rosesTxt.SetText(PlayerPrefs.GetInt("Roses").ToString());
        keysTxt.SetText(GetComponent<PlayerStash>().GetKeys().ToString());
        soulsTxt.SetText(PlayerPrefs.GetInt("Souls").ToString());
        slider.value = _currentHp / PlayerPrefs.GetFloat("MaxHp");;
        RefreshHearts();
    }

    private void ManageHearts()
    {
        if (_currentHp > 0) return;
        _heartCount -= 1;
        RefreshHearts();
        ResetHp();
        if (_heartCount > 0) return;
        Destroy(gameObject);
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
    }

    private void Update()
    {
        ManageHearts();
        RefreshHearts();
        UpdateUI();
        LoadMenuHandler.SaveToCurrentSlot();
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

    public void SetHp(float newHp)
    {
        _currentHp = newHp;
    }

    public int getHearts()
    {
        return _heartCount;
    }

    public float getCurrentHp()
    {
        return _currentHp / maxHp;
    }

    public void SetMeleeDamage(float damage) { meleeDamage = damage; }
    
    public void SetRangedDamage(float damage) { _rangedDamage = damage; }
    
    public void SetDashDamage(float damage) { _dashDamage = damage; }
    
    public void SetCurrentResistance(float resistance) { _currentResistance = resistance; }

    public void enablePlasmaSwimming()
    {
        PlayerPrefs.SetInt("CanSwim", 1);
        canSwimInPlasma = true;
        LoadMenuHandler.SaveToCurrentSlot();
    }

    public bool getPlasmaStatus()
    {
        return canSwimInPlasma;
    }
}
