using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlayerScripts;
using UnityEngine;

public class SkeletonBehavior : Enemy, IDamageable
{
    [SerializeField] private float damage = 25f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float idleWonder = 5f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float windupDuration = 0.5f;
    [SerializeField] private float attacksGap = 0.25f;
    [SerializeField] private float disengagementDuration = 1f;
    private enum SkeletonStates { IDLE,AGGREVATED,WIND_UP,FIRST_ATTACK,BETWEEN_ATTACKS,SECOND_ATTACK,DISENGAGING}
    private bool _isLookingRight;
    private GameObject _player;
    private SkeletonStates _skeletonState;
    private Vector2 _idlePosition;
    private Vector2 _nextIdlePosition;
    private SpriteRenderer _renderer; //used only when no animations or assets. Change later for animation
    private float _windupTimer;
    private float _gapTimer;
    private float _disengagementTimer;
    private float _attackOffset; 
    private StuckDetector _stuckDetector;
    
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _isLookingRight = true;
        _stuckDetector = GetComponent<StuckDetector>();
        BecomeIdle();
    }

    private void Update()
    {
        UpdateOrientation();
        switch (_skeletonState)
        {
            case SkeletonStates.IDLE:
                IdleMove();
                break;
            case SkeletonStates.AGGREVATED:
                transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, _player.transform.position) <= attackDistance)
                {
                    _skeletonState = SkeletonStates.WIND_UP;
                    _windupTimer = windupDuration;
                }
                break;
            case SkeletonStates.WIND_UP:
                _renderer.color = Color.red;
                _windupTimer -= Time.deltaTime;
                if (_windupTimer <= 0)
                {
                    _skeletonState = SkeletonStates.FIRST_ATTACK;
                }
                break;
            case SkeletonStates.FIRST_ATTACK:
                _renderer.color = Color.magenta;
                SwingArm();
                _gapTimer = attacksGap;
                _skeletonState = SkeletonStates.BETWEEN_ATTACKS;
                break;
            case SkeletonStates.BETWEEN_ATTACKS:
                _renderer.color = Color.green;
                _gapTimer -= Time.deltaTime;
                if (_gapTimer <= 0)
                {
                    _skeletonState = SkeletonStates.SECOND_ATTACK;
                }
                break;
            case SkeletonStates.SECOND_ATTACK:
                _renderer.color = Color.cyan;
                SwingArm();
                _disengagementTimer = disengagementDuration;
                _skeletonState = SkeletonStates.DISENGAGING;
                break;
            case SkeletonStates.DISENGAGING:
                _renderer.color = Color.yellow;
                _disengagementTimer -= Time.deltaTime;
                if (_disengagementTimer <= 0)
                {
                    _renderer.color = Color.white;
                    _skeletonState = SkeletonStates.AGGREVATED;
                }
                break;
            
        }
    }

    private void SwingArm()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(
            new Vector2(transform.position.x+_attackOffset,transform.position.y),
            new Vector2(attackDistance, attackDistance),
            0
            );
        foreach (var collider in colliders)
        {
            Attack(collider.gameObject);
        }
    }

    private void UpdateOrientation()
    {
        _isLookingRight = _skeletonState == SkeletonStates.IDLE ? _nextIdlePosition.x > transform.position.x : _player.transform.position.x > transform.position.x;
        _attackOffset = 0.5f * attackDistance;
        _attackOffset *= _isLookingRight ? 1 : -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")){return;}
        _player = other.gameObject;
        _skeletonState = SkeletonStates.AGGREVATED;
    }
    

    protected override void Attack(GameObject go)
    {
        if (!go.CompareTag("Player")){return;}
        go.GetComponent<PlayerStats>().DealDamage(damage);
    }

    public override void ChangeHp(float damage)
    {
        Hp -= damage;
        if (Hp < 0)
        {
            Destroy(gameObject);
        }
    }

    private void BecomeIdle()
    {
        _renderer.color = Color.white;
        _player = null;
        _skeletonState = SkeletonStates.IDLE;
        _idlePosition = transform.position;
        _nextIdlePosition = _idlePosition;
    }

    private void IdleMove()
    {
        bool tooFar = Vector2.Distance(transform.position, _nextIdlePosition) > 0.1f;
        bool withinArea = Math.Abs(_idlePosition.x - transform.position.x) <= idleWonder;
        bool isStuck = _stuckDetector.IsStuckX();
        if (isStuck)
        {
            float scale = (_isLookingRight ? UnityEngine.Random.Range(0, 5) : UnityEngine.Random.Range(5, 11))/10f;
            _nextIdlePosition=new Vector2(_idlePosition.x + scale* idleWonder - idleWonder*0.5f,_idlePosition.y);
            _stuckDetector.HoldUp();
            _stuckDetector.Clear();
        }
        else
        {
            if ( tooFar && withinArea)
            {
                transform.position = Vector2.MoveTowards(transform.position, _nextIdlePosition, speed * Time.deltaTime);
            }
            else
            {
                _nextIdlePosition=new Vector2(_idlePosition.x + UnityEngine.Random.Range(0, 11)/10f * idleWonder - idleWonder*0.5f,_idlePosition.y);
            }
        }
    }
}
