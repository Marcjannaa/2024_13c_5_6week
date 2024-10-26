using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class ColorMode : MonoBehaviour
{
    public enum PlayerState { Attacking, Idle, HighHp, MediumHp, LowHp } // w zaleznosci od zabitych przeciwnikow, zebranych deadrosy
    
    [SerializeField] private Light2D ld;
    [SerializeField] private float intensity = 0.5f;
    private PlayerState _playerState;
    private float _alpha;
    private void Start()
    {
        var color = new Color(255, 23, 35, 255);
        ld.color = color;
        ld.intensity = intensity;
        ld.falloffIntensity = 1f;
        ld.volumeIntensityEnabled = true;
        ld.volumeIntensity = 0.0001f;
    }

    public void UpdateColor()
    {
        ld.color = PickColor(_playerState);
        var hp = gameObject.GetComponentInParent<PlayerStats>().getCurrentHp();
        _alpha = 255f * hp;
        intensity *= hp;
        var hearts = gameObject.GetComponentInParent<PlayerStats>().getHearts();
        _playerState = hearts switch 
        {
                1 => PlayerState.LowHp,
                2 => PlayerState.MediumHp,
                3 => PlayerState.HighHp,
                _ => _playerState 
        };
        ld.color = PickColor(_playerState);
    }

    private Color PickColor(PlayerState ps)
    {
        return ps switch
        {
            PlayerState.Attacking => new Color(255f, 10f, 35f, 255),
            PlayerState.HighHp => new Color(60f, 0f, 255f, _alpha),
            PlayerState.MediumHp => new Color(255f, 100f, 0f, _alpha),
            PlayerState.LowHp => new Color(200f, 0f, 0f, _alpha),
            _ => new Color(255f, 255f, 255f, _alpha)
        };
    }

    public void setPlayerState(PlayerState ps)
    {
        _playerState = ps;
    }
}
