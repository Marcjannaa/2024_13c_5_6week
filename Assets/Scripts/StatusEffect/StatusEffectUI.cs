using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//tutorial:
//https://youtu.be/M9vWYBrthpU
public class StatusEffectIconCache
{
    public GameObject statusIconContainer;
    public Image statusBuildupFill;
    public Image statusActiveTimerFill;
    public Image statusIcon;

    public StatusEffectIconCache(GameObject statusIconContainer, Image statusBuildupFill, Image statusActiveTimerFill, Image statusIcon)
    {
        this.statusIconContainer = statusIconContainer;
        this.statusBuildupFill = statusBuildupFill;
        this.statusActiveTimerFill = statusActiveTimerFill;
        this.statusIcon = statusIcon;
    }
}
public class StatusEffectUI : MonoBehaviour
{
    [SerializeField] private GameObject statusEffectIconTemplate;
    //[SerializeField] private SerializableDictionary<StatusEffectType, Sprite> statusEffectSpriteDict; //powiązanie typu z ikoną. Dodać w edytorze (z rozszerzeniem)lub w kodzie
    [SerializeField] private Dictionary<StatusEffectType, Sprite> statusEffectSpriteDict;
    private Camera _mainCamera;
    private StatusEffectManager _statusEffectManager;
    private Dictionary<StatusEffectSO, StatusEffectIconCache> statusEffectTypeToIconDict;
    private void Start()
    {
        _mainCamera = Camera.main;
        statusEffectTypeToIconDict = new Dictionary<StatusEffectSO, StatusEffectIconCache>();
        _statusEffectManager = GetComponentInParent<StatusEffectManager>();
        _statusEffectManager.ActivateStatus += OnActivate;
        _statusEffectManager.UpdateStatus += OnUpdateStatus;
        _statusEffectManager.DeactivateStatus += OnDeactivate;
    }

    private void Update()
    {
        transform.rotation=Quaternion.LookRotation(transform.parent.position-_mainCamera.transform.position);
    }

    private StatusEffectIconCache CreateStatusIcon(StatusEffectSO statusEffect)
    {
        if (statusEffectTypeToIconDict.ContainsKey(statusEffect))
        {
            var cache = statusEffectTypeToIconDict[statusEffect];
            cache.statusIconContainer.SetActive(true);
            return cache;
        }

        GameObject createdStatusIcon = Instantiate(statusEffectIconTemplate, transform);
        GameObject statusActiveTimer = createdStatusIcon.transform.Find("StatusActiveTimer").gameObject;
        Image statusBuildUpRadialFill = createdStatusIcon.GetComponent<Image>();
        statusBuildUpRadialFill.fillAmount = 0;
        Image statusActiveTimerRadialFill = statusActiveTimer.GetComponent<Image>();
        statusActiveTimerRadialFill.fillAmount = 0;
        Image statusIcon = createdStatusIcon.transform.Find("Icon").GetComponent<Image>();
        statusIcon.sprite = statusEffectSpriteDict[statusEffect.statusEffectType];
        createdStatusIcon.SetActive(true);
        return new StatusEffectIconCache(createdStatusIcon, statusBuildUpRadialFill, statusActiveTimerRadialFill,
            statusIcon);
    }

    private void OnActivate(StatusEffectSO statusEffect,float buildUp)
    {
        StatusEffectIconCache cache = CreateStatusIcon(statusEffect);
        statusEffectTypeToIconDict[statusEffect] = cache;
        OnUpdateStatus(statusEffect, buildUp, 0);
    }

    private void OnUpdateStatus(StatusEffectSO statusEffect, float buildUp, float duration)
    {
        statusEffectTypeToIconDict[statusEffect].statusBuildupFill.fillAmount = buildUp;
        statusEffectTypeToIconDict[statusEffect].statusActiveTimerFill.fillAmount = duration;
    }
    

    private void OnDeactivate(StatusEffectSO statusEffect)
    {
        statusEffectTypeToIconDict[statusEffect].statusIconContainer.SetActive(false);
    }
}
