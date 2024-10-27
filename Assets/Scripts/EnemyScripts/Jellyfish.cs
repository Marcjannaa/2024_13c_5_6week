using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;


public class Jellyfish : Enemy
{
    [SerializeField] private List<Vector2> positions = new List<Vector2>() { new(0f, 0f), new(0f, 0f), new(0f, 0f) };
    private float atkDelay;
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    private float contactDamageDelay = 0.5f;
    private float counter = 0f;
    private float dmg = 30f;
    private float distance;
    private bool canAttack = true;
    private bool canMoveTo = true;
    private int action = 1;
    private Random rnd = new Random();
    private static float moveTowardsPlayerDuration = 5f;
    private static float attackDelay = 2f;

    
    private enum JellyAttack
    {
        Sweep,
        Drain,
        Shoot,
        Move
    }
    
    void Start()
    {
        Hp = MaxHp;
    }
    
    void Update()
    {
        

        switch (action)
        {
            case 0:
                print("atak");
                if (canAttack)
                {
                    StartCoroutine(attackCoroutine());
                }
                break;
            case 1:
                print("ruch");
                if (canMoveTo)
                {
                    int randomPos = rnd.Next(0, 3);
                    StartCoroutine(moveToPositionCoroutine(positions[randomPos]));
                }
                break;
        }

    }
    
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")){return;}
        if (counter <= 0f)
        {
            Attack(other.gameObject);
            counter = contactDamageDelay;
        }else counter -= Time.deltaTime;
    }
    protected override void Attack(GameObject go)
    {
        go.GetComponent<PlayerStats>().DealDamage(dmg);
    }

    public override void ChangeHp(float damage)
    {
        if (Hp - damage <= 0)
        {
            Destroy(gameObject);
            return;
        }
        Hp -= damage;
//        gameObject.GetComponent<HpBar>().UpdateBar(Hp, MaxHp);
    }



    void movingAttack()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
//        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
//        transform.rotation = Quaternion.Euler(Vector3.forward * rotationAngle);
    }

    private JellyAttack rollAttack()
    {
        int atk = rnd.Next(0, 100);
        switch (atk)
        {
            case <101:
                return JellyAttack.Move;
        }

        return JellyAttack.Move;
    }

    private IEnumerator attackCoroutine()
    {
        canAttack = false;


        JellyAttack ja = rollAttack();

        switch (ja)
        {
            case JellyAttack.Move:
                yield return StartCoroutine(moveAttackCoroutine());
                break;
            case JellyAttack.Sweep:
                break;
            case JellyAttack.Shoot:
                break;
            case JellyAttack.Drain:
                break;
        }
        
        yield return new WaitForSeconds(attackDelay);
        action = 1;
        canAttack = true;
    }

    private IEnumerator moveAttackCoroutine()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < moveTowardsPlayerDuration)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
        yield return null;
    }

    private IEnumerator moveToPositionCoroutine(Vector2 newPosition)
    {
        canMoveTo = false;
        Vector3 v3 = new Vector3(newPosition.x, newPosition.y, 0);
        while (transform.position.x != newPosition.x && transform.position.y != newPosition.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, v3, 2 * speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(attackDelay);
        action = 0;
        canMoveTo = true;
    }
}


