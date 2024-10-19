using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : Enemy
{
    [SerializeField] protected List<float> phaseChangeThresholds = new List<float>(){0.5f};
    protected int _currentPhase; //WARNING: phases are coounted from 0. It has to match thresholdList 
    protected SpriteRenderer _renderer;
    protected GameObject _player;
    protected BossGateBehavior[] _gates;
    protected float _currentDamage=0f; //used for attack function

    private void Awake()
    {
        phaseChangeThresholds.Sort(); // I don't trust future me
        phaseChangeThresholds.Reverse();
        _currentPhase = 0;
    }

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.enabled = false;
        _gates = GetComponentsInChildren<BossGateBehavior>();
    }

    public void Aggrevate(GameObject player)
    {
        _player = player;
        _renderer.enabled = true;
        foreach (BossGateBehavior gate in _gates)
        {
            gate.Lock();
        }

        //StartCoroutine(Fight());
    }

    protected abstract IEnumerator Fight();

    protected void Die()
    {
        Destroy(gameObject);
    }

    protected bool IsAggrevated()
    {
        return _player is not null;
    }

    protected override void Attack(GameObject go)
    {
        if(!go.CompareTag("Player")){return;}
        go.GetComponent<PlayerStats>().DealDamage(_currentDamage);
    }

    public override void ChangeHp(float damage)
    {
        Hp -= damage;
        if (_currentPhase < phaseChangeThresholds.Count && Hp / MaxHp < phaseChangeThresholds[_currentPhase])
        {
            _currentPhase++;
        }
        if (Hp <= 0)
        {
            Die();
        }
    }
}
