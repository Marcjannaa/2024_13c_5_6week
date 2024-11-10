using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultDataProvider : MonoBehaviour
{
    [SerializeField] private float defaultMaxHp = 100f;
    [SerializeField] private float defaultMeleeDamage=5f;

    [SerializeField] private float defaultStamina=1f; //nie wiem co to jest

    [SerializeField] private float defaultDashDuration=5f;

    [SerializeField] private int defaultSoulsCount = 0;
    [SerializeField] private int defaultRosesCount = 0;
    [SerializeField] private int defaultKeysCount = 0;
    private void Awake()
    {
        int counter = 0;
        counter+=CheckForFloat("MaxHp",defaultMaxHp)?1:0;
        counter+=CheckForFloat("MeleeAttack",defaultMeleeDamage)?1:0;
        counter+=CheckForFloat("DashDuration",defaultDashDuration)?1:0;
        counter+=CheckForFloat("Stamina",defaultStamina)?1:0;
        counter+=CheckForInt("Souls",defaultSoulsCount)?1:0;
        counter+=CheckForInt("Roses",defaultRosesCount)?1:0;
        counter+=CheckForInt("Keys",defaultKeysCount)?1:0;
        Debug.Log("Values checked, "+counter+" were missing and were added");
    }

    private bool CheckForFloat(string parameter, float value)
    {
        if (!PlayerPrefs.HasKey(parameter)) {
            PlayerPrefs.SetFloat(parameter, value);
            return false;
        }
        return true;
    }
    private bool CheckForInt(string parameter, int value)
    {
        if (!PlayerPrefs.HasKey(parameter)) {
            PlayerPrefs.SetInt(parameter, value);
            return false;
        }
        return true;
    }
}
