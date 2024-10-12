using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//tutorial:
//https://youtu.be/M9vWYBrthpU
public class StatusEffectManager : MonoBehaviour
{
    /* po rozszerzeniu edytora o serializowalne słowniki
    [SerializeField] private SerializableDictionary<StatusEffectType, StatusEffectSO> _statusEffectToApplyDict=new();
    private SerializableDictionary<StatusEffectType, StatusEffectSO> _enabledEffects = new();
    private SerializableDictionary<StatusEffectType, StatusEffectSO> _statusEffectCasheDict = new Dictionary<StatusEffectType, StatusEffectSO>();
     */
    private Dictionary<StatusEffectType, StatusEffectSO> _statusEffectToApplyDict=new();
    private Dictionary<StatusEffectType, StatusEffectSO> _enabledEffects = new();
    private Dictionary<StatusEffectType, StatusEffectSO> _statusEffectCasheDict = new Dictionary<StatusEffectType, StatusEffectSO>();
    [SerializeField, Tooltip("Time between running updateCall in StatusEffectSO")] private float _interval=0.1f;
    private float _currentInterval;
    private float _lastInterval;
    public UnityAction<StatusEffectSO, float> ActivateStatus;
    public UnityAction<StatusEffectSO, float,float> UpdateStatus;
    public UnityAction<StatusEffectSO> DeactivateStatus;
    
    private void OnStatusTriggerBuildUp(StatusEffectType status, float buildup)
    {
        if (!_enabledEffects.ContainsKey(status))
        {
            var effectToAdd = CreateEffectObject(status, _statusEffectToApplyDict[status]);
            _enabledEffects.Add(status,effectToAdd);
            //UI update here
            ActivateStatus?.Invoke(effectToAdd,effectToAdd.GetCurrentDurationNormalized());
        }

        if (!_enabledEffects[status].isEffectActive)
        {
            var enabled = _enabledEffects[status];
            enabled.AddBuildup(buildup,gameObject);
            //UI update here
            UpdateStatus?.Invoke(enabled,enabled.GetCurrentDurationNormalized(),enabled.GetCurrentDurationNormalized());
        }
        else
        {
            //status active
        }
    }

    private StatusEffectSO CreateEffectObject(StatusEffectType status, StatusEffectSO effectSo)
    {
        if (!_statusEffectCasheDict.ContainsKey(status))
        {
            _statusEffectCasheDict.Add(status,Instantiate(effectSo));
        }

        return _statusEffectCasheDict[status];
    }

    public void UpdateEffect(GameObject target)
    {
        foreach (var effect in _enabledEffects.ToList())
        {
            effect.Value.UpdateCall(target,_interval);
            UpdateStatus?.Invoke(effect.Value,effect.Value.GetCurrentThresholdNormalized(),effect.Value.GetCurrentDurationNormalized()); //UI update
            if (effect.Value.CanStatusVisualBeRemoved())
            {
                RemoveEffect(effect.Key);
            }
        }
    }

    public void RemoveEffect(StatusEffectType status)
    {
        if (_enabledEffects.ContainsKey(status))
        {
            _enabledEffects[status].RemoveEffect(gameObject);
            //UI update here
            DeactivateStatus?.Invoke(_enabledEffects[status]);

            _enabledEffects.Remove(status);
        }
    }

    private void Update()
    {
        _currentInterval += Time.deltaTime;
        if (_currentInterval > _lastInterval + _interval)
        {
            UpdateEffect(gameObject);
            _lastInterval = _currentInterval;
        }
    }
}
