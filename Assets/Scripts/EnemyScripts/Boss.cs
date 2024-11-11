using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public abstract class Boss : Enemy, IDamageable
{
    [SerializeField] protected List<float> phaseChangeThresholds = new List<float>(){0.5f};
    [SerializeField] protected List<GameObject> gates;
    protected GameObject musicBox;
    protected int _currentPhase; //WARNING: phases are coounted from 0. It has to match thresholdList 
    protected SpriteRenderer _renderer;
    protected GameObject _player;
    protected float _currentDamage=0f; //used for attack function
    protected bool _fightStarted;
    protected HpBar _hpBar;
    private void Awake()
    {
        _fightStarted = false;
        phaseChangeThresholds.Sort(); // I don't trust future me
        phaseChangeThresholds.Reverse();
        _currentPhase = 0;
    }

    private void Start()
    {
        musicBox =  GameObject.FindGameObjectWithTag("MusicBox");
        Debug.Log("music box wykryty");

        _hpBar = GetComponent<HpBar>();
        _hpBar.SetVisibility(false);

        _renderer = GetComponent<SpriteRenderer>();
        _renderer.enabled = false;
    }

    public void Aggrevate(GameObject player)
    {
        if(!_fightStarted)
        {
            _hpBar.SetVisibility(true);
            _fightStarted = true;
            _player = player;
            _renderer.enabled = true;
            foreach (GameObject gate in gates)
            {
                print("zamykam brame");
                gate.GetComponent<BossGateBehavior>().Lock();
            }

            StartCoroutine(Fight());
        }
    }

    protected abstract IEnumerator Fight();

    protected void Die()
    {
        foreach (GameObject gate in gates)
        {
            gate.GetComponent<BossGateBehavior>().Release();
            musicBox.GetComponent<MusicBox>().PlayMusicForCurrentScene();
        }
        Destroy(gameObject);
    }

    protected bool IsAggrevated()
    {
        return _player is not null;
    }

    protected bool IsLookingRight()
    {
        return _player is not null?transform.position.x < _player.transform.position.x:true;
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

        if (_hpBar is not null)
        {
            _hpBar.UpdateBar(Hp,MaxHp);
        }

    }

    protected float HorizontalDistanceToPlayer()
    {
        return Math.Abs(transform.position.x - _player.transform.position.x);
    }
    protected float VerticalDistanceToPlayer()
    {
        return Math.Abs(transform.position.y - _player.transform.position.y);
    }

    protected float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, _player.transform.position);
    }
}
