using System;
using PlayerScripts;
using UnityEngine;
using Random = System.Random;

public class BatBehavior : Enemy, IDamageable
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float attackSpeed=6f;
    [SerializeField] private float attackCooldown=2f;
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float idleWonderX = 2f;
    [SerializeField] private float idleWonderY = 2f;
    private enum BatAttackSequence {IDLE,AGGREVATED,CHARGING,DISANGAGING,READY}
    private Vector2 _attackStartPos;
    private Vector2 _idlePos;
    private Vector2 _nextIdlePos;
    private float _attackCooldownCounter;
    private BatAttackSequence _attackState;
    private GameObject _player;
    private StuckDetector _stuckDetector;
    private Vector2 _lookingDirection;
    private void Start()
    {
        _stuckDetector = GetComponent<StuckDetector>();
        BecomeIdle();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            Debug.Log("oof");
        }
        if (!other.gameObject.CompareTag("Player")){return;}

        if(_attackCooldownCounter<=0){
            Attack(other.gameObject);
            _attackCooldownCounter = attackCooldown;
            _attackState = BatAttackSequence.DISANGAGING;
        }
    }

    private void Update() //BUG: Usunięcie playera ze sceny i powrót psuje zachowanie
    {
        RefreshLookingDirection();
        switch (_attackState)
        {
            case BatAttackSequence.IDLE:
                IdleMove();
                break;
            case BatAttackSequence.AGGREVATED:
                transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, _player.transform.position) <= attackDistance)
                {
                    _attackState = BatAttackSequence.CHARGING;
                    _attackStartPos = transform.position;
                }
                break;
            case BatAttackSequence.CHARGING:
                transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, attackSpeed * Time.deltaTime);
                break;
            case BatAttackSequence.DISANGAGING:
                transform.position = Vector2.MoveTowards(transform.position, _attackStartPos, attackSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, _attackStartPos) == 0f)
                {
                    _attackState = BatAttackSequence.READY;
                }
                break;
            case BatAttackSequence.READY:
                _attackCooldownCounter -= Time.deltaTime;
                if (_attackCooldownCounter <= 0f)
                {
                    _attackState = BatAttackSequence.AGGREVATED;
                }
                break;
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")){return;}
        _player = other.gameObject;
        _attackState = BatAttackSequence.AGGREVATED;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")){return;}
        BecomeIdle();
    }

    protected override void Attack(GameObject go)
    {
        if (!go.CompareTag("Player")){return;} //not a player, it should not be attacked
        go.GetComponent<PlayerStats>().DealDamage(damage);
    }

    public override void ChangeHp(float damage)
    {
        Die(); //bats suppose to be one shot
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void BecomeIdle()
    {
        _attackState = BatAttackSequence.IDLE;
        _player = null;
        _idlePos = transform.position;
        _nextIdlePos = _idlePos;
    }

    private void RefreshLookingDirection()
    {

        float x = transform.position.x - _nextIdlePos.x;
        float y = transform.position.y - _nextIdlePos.y;
        if (_attackState != BatAttackSequence.IDLE)
        {
            x = transform.position.x - _player.transform.position.x;
            y = transform.position.y - _player.transform.position.y;
        }
        _lookingDirection = new Vector2(x, y);
    }

    private void IdleMove()
    {
        bool tooFar = Vector2.Distance(transform.position, _nextIdlePos) <= 0;
        bool withinAreaX = Math.Abs(transform.position.x - _idlePos.x) <= idleWonderX;
        bool withinAreaY = Math.Abs(transform.position.y - _idlePos.y) <= idleWonderY;
        if(!_stuckDetector.IsStuck()){
            if (tooFar && withinAreaX && withinAreaY)
            {
                float newX = _idlePos.x + UnityEngine.Random.Range(0, 11) / 10f * idleWonderX - idleWonderX * 0.5f;
                float newY = _idlePos.y + UnityEngine.Random.Range(0, 11) / 10f * idleWonderY - idleWonderY * 0.5f;
                _nextIdlePos = new Vector2(newX, newY);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, _nextIdlePos, speed * Time.deltaTime);
            }
        }
        else
        {
            float scaleX = (_stuckDetector.IsStuckX()
                ? (_lookingDirection.x > 0 ? UnityEngine.Random.Range(0, 5) : UnityEngine.Random.Range(5, 11))
                : UnityEngine.Random.Range(0, 11))/10f;
            float scaleY = (_stuckDetector.IsStuckY()
                ? (_lookingDirection.y > 0 ? UnityEngine.Random.Range(0, 5) : UnityEngine.Random.Range(5, 11))
                : UnityEngine.Random.Range(0, 11))/10f;
            float newX = _idlePos.x + scaleX * idleWonderX - idleWonderX * 0.5f;
            float newY = _idlePos.y + scaleY * idleWonderY - idleWonderY * 0.5f;
            _nextIdlePos = new Vector2(newX, newY);
            _stuckDetector.Clear();
            _stuckDetector.HoldUp();
        }
    }
    
    
}
