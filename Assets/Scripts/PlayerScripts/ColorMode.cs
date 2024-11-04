using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
    private ParticleSystem _particleSystem;
    private ParticleSystem.MainModule _main;
    private void Start()
    {
        _particleSystem = gameObject.GetComponent<ParticleSystem>();
        _main = _particleSystem.main;
        ld.color = PickColor(_playerState);
        SetParticles();
        ld.intensity = intensity;
        ld.falloffIntensity = 1f;
        ld.volumeIntensityEnabled = true;
        ld.volumeIntensity = 0.0001f;
    }

    private void SetParticles()
    {
        _main.startColor = PickColor(_playerState);
        _main.loop = true;
        _main.simulationSpace = ParticleSystemSimulationSpace.World;
        _main.startLifetime = 2f;
        _main.playOnAwake = true;
        _main.startSize = 1f;
        _main.startSpeed = 0.5f;
        _main.maxParticles = 100;
    }
    public void UpdateColor()
    {
        ld.color = PickColor(_playerState);
        SetParticleColor(PickColor(_playerState));
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
        SetParticleColor(PickColor(_playerState));
    }

    private Color PickColor(PlayerState ps)
    {
        return ps switch
        {
            PlayerState.HighHp => new Color(60f, 0f, 255f, _alpha),
            PlayerState.MediumHp => new Color(255f, 100f, 0f, _alpha),
            PlayerState.LowHp => new Color(200f, 0f, 0f, _alpha),
            _ => new Color(255f, 255f, 255f, _alpha)
        };
    }
    
    private void SetParticleColor(Color color)
    {
        var mainModule = GetComponent<ParticleSystem>().main;
        var gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0] = new GradientColorKey(color, 0.0f); 
        colorKeys[1] = new GradientColorKey(new Color(color.r / 10, color.g / 10, color.b / 10, _alpha), 0.5f);  
        
        gradient.colorKeys = colorKeys;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(gradient);
    }

    public void SetPlayerState(PlayerState ps) { _playerState = ps; }
}
