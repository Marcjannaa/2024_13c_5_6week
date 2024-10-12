using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//tutorial:
//https://youtu.be/M9vWYBrthpU
public enum StatusEffectType{Dash}
public abstract class StatusEffectSO : ScriptableObject
{
    public StatusEffectType statusEffectType;
    public float activationThreshold;
    public float thresholdReductionMultiplier = 1f;
    public float thresholdReductionEverySecond = 1f;
    public float activeDuration;
    public GameObject visualEffectPrefab;
    
    private float _currentThreshold;
    private float _remainingDuration;
    private GameObject _vfxPlaying;
    [HideInInspector] public bool isBuildUpOnlyShow;
    [HideInInspector] public bool isEffectActive;
    public float tickInterval = 0.5f;
    private float tickIntervalCoolDown = 0.5f;

    public virtual void AddBuildup(float buildup, GameObject target)
    {
        isBuildUpOnlyShow = true;
        _currentThreshold += buildup;
        if (_currentThreshold>=activationThreshold)
        {
            ApplyEffect(target);
        }
    }

    public virtual void ApplyEffect(GameObject target)
    {
        isEffectActive = true;
        if (visualEffectPrefab != null)
        {
            _vfxPlaying = Instantiate(visualEffectPrefab, target.transform.position,Quaternion.identity,target.transform);
        }
    }

    public void UpdateCall(GameObject target,float ticks)
    {
        if (isEffectActive)
        {
            isBuildUpOnlyShow = false;
            _remainingDuration -= ticks;
            isEffectActive = _remainingDuration > 0;
        }
        else
        {
            _currentThreshold -= ticks * thresholdReductionEverySecond*thresholdReductionMultiplier;
            isBuildUpOnlyShow = _currentThreshold > 0;
        }
        tickIntervalCoolDown += ticks;
        if (tickIntervalCoolDown >= tickInterval)
        {
            UpdateEffect(target);
            tickIntervalCoolDown = 0;
        }
    }

    public virtual void UpdateEffect(GameObject target)
    {
        
    }
    public virtual void RemoveEffect(GameObject target)
    {
        isEffectActive = false;
        _currentThreshold = 0;
        _remainingDuration = 0;
        if (_vfxPlaying != null)
        {
            Destroy(_vfxPlaying);
        }
    }

    public virtual bool CanStatusVisualBeRemoved()
    {
        return !isEffectActive && !isBuildUpOnlyShow;
    }

    public float GetCurrentThresholdNormalized()
    {
        return _currentThreshold / activationThreshold;
    }
    public float GetCurrentDurationNormalized()
    {
        return _remainingDuration / activeDuration;
    }
}
