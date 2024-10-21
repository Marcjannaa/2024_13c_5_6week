using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGateBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _boss;
    private GameObject _physical;
    void Start()
    {
        _physical = transform.GetChild(0).gameObject;
        _physical.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player")){return;}
        _boss.GetComponent<Boss>().Aggrevate(other.gameObject);
    }

    public void Lock()
    {
        _physical.SetActive(true);
    }

    public void Release()
    {
        Destroy(gameObject);
    }
}
