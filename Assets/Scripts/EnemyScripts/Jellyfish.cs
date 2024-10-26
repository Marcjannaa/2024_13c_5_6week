using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Jellyfish : Enemy
{
    [SerializeField]private List<Vector2> positions = new List<Vector2>() { new(0f, 0f), new(0f, 0f), new(0f, 0f) };
    private float atkDelay;
    [SerializeField] private float speed;
    private float dmgDealy = 0.5f;
    private float counter = 0f;
    private float dmg = 30f;
    
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) { return; }
        

    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")){return;}
        if (counter <= 0f)
        {
            Attack(other.gameObject);
            counter = dmgDealy;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void movingAttack(GameObject go)
    {
        var multi = go.gameObject.transform.position.x > transform.position.x ? 1 : -1;
        transform.position += new Vector3(multi * speed * Time.deltaTime, 0, 0);
    }
}


