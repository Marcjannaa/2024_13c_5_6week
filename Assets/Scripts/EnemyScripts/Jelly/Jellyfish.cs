using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using Random = System.Random;


public class Jellyfish : Enemy, IDamageable
{
    [SerializeField] private List<Vector2> positions = new List<Vector2>() { new(0f, 0f), new(0f, 0f), new(0f, 0f) };
    private float atkDelay;
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    private float contactDamageDelay = 1f;
    private float counter = 0f;
    private float dmg = 15f;
    private float distance;
    private bool canAttack = true;
    private bool canMoveTo = true;
    private bool chase = false;
    private int action = 1;
    private Random rnd = new Random();
    private static float moveTowardsPlayerDuration = 3f;
    private static float attackDelay = 1f;
    private GameObject musicBox;
    
    private Transform[] tentacles;
    [SerializeField] protected List<GameObject> gates;

    
    private enum JellyAttack
    {
        Sweep,
        Shoot,
        Move
    }
    
    void Start()
    {
        
        Hp = MaxHp;
        int childCount = transform.childCount ;
        tentacles = new Transform[childCount-2];

        
        
        bool headfound = false;
        for (int i = 0; i < childCount-1; i++)
        {
            if (transform.GetChild(i).name != "Head" && !headfound)
            {
                tentacles[i] = transform.GetChild(i).GetChild(0); 
            }else
            {
                headfound = true;
            }

            if (headfound && transform.GetChild(i).name != "Head")
            {
                tentacles[i-1] = transform.GetChild(i).GetChild(0); 
            }
            
            
        }

        StartCoroutine(moveToPositionCoroutine(positions[0]));

        foreach (var gate in gates)
        {
            gate.SetActive(true);
        }
        musicBox =  GameObject.FindGameObjectWithTag("MusicBox");
        musicBox.GetComponent<MusicBox>().PlayBossMusic("Jelly");
    }

    void Update()
    {
        if (Hp > 200)
        {
            switch (action)
            {
                case 0:
                    if (canAttack)
                    {
                        StartCoroutine(attackCoroutine());
                    }
                    break;
                case 1:
                    if (canMoveTo)
                    {
                        int randomPos = rnd.Next(0, 3);
                        StartCoroutine(moveToPositionCoroutine(positions[randomPos]));
                    }
                    break;
            }
        }
        else 
        {
            if (!chase)
            {
                chase = true;
                StartCoroutine(chaseSequenceCoroutine());
                gates[1].SetActive(false);
            }
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
            musicBox.GetComponent<MusicBox>().PlayMusicForCurrentScene();
            Destroy(gameObject);
            return;
        }
        Hp -= damage;
        gameObject.GetComponent<HpBar>().UpdateBar(Hp, MaxHp);
    }

    private JellyAttack rollAttack()
    {
        int atk = rnd.Next(0, 100);
        
        if (atk < 40)
            return JellyAttack.Move;
        else if (atk < 70)
            return JellyAttack.Sweep;
        else
            return JellyAttack.Shoot;
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
                yield return StartCoroutine(sweepAttackCoroutine());
                break;
            case JellyAttack.Shoot:
                yield return StartCoroutine(shootAttackCoroutine());
                break;
        }
        
        yield return new WaitForSeconds(attackDelay);
        action = 1;
        canAttack = true;
    }

    private IEnumerator shootAttackCoroutine()
    {
        bool shot = false;
        while (!shot)
        {
            int counter = 0;
            
            foreach (var tentacle in tentacles)
            {
                if (counter < tentacles.Length / 2)
                {
                    tentacle.GetComponent<Tentacle>().Rotate(0);
                }
                else
                {
                    tentacle.GetComponent<Tentacle>().Rotate(1);
                }
                counter++;
            }


            yield return new WaitForSeconds(1);
            
            foreach (var tentacle in tentacles)
            {
                tentacle.GetComponent<Tentacle>().Shoot(player);
            }

            foreach (var tentacle in tentacles)
            {
                StartCoroutine(tentacle.GetComponent<Tentacle>().ReturnToInitialRotation());
            }

            shot = true;
        }

        yield return null;
    }


    private IEnumerator sweepAttackCoroutine()
    {
        if (transform.position == new Vector3(positions[1].x, positions[1].y, 0))
        {
            StartCoroutine(tentacles[tentacles.Length-1].GetComponent<Tentacle>().Sweep("right"));
        }else if (transform.position == new Vector3(positions[0].x, positions[0].y, 0))
        {
            StartCoroutine(tentacles[tentacles.Length-1].GetComponent<Tentacle>().Sweep("right"));
            StartCoroutine(tentacles[0].GetComponent<Tentacle>().Sweep("left"));
        }else if (transform.position == new Vector3(positions[2].x, positions[2].y, 0))
        {
            StartCoroutine(tentacles[0].GetComponent<Tentacle>().Sweep("left"));
        }
        yield return new WaitForSeconds(1);
        yield return null;
    }

    private IEnumerator moveAttackCoroutine()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < moveTowardsPlayerDuration)
        {
            Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y + 2f, 0);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, 2 * speed * Time.deltaTime);
            
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
        yield return null;
    }
    
    private IEnumerator chaseSequenceCoroutine()
    {
        while (Hp > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            
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


