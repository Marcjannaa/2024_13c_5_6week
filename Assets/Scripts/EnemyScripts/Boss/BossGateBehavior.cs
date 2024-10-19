using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGateBehavior : MonoBehaviour
{
    private GameObject _boss;
    private GameObject _physical;
    void Start()
    {
        _boss = transform.transform.parent.gameObject;
        _physical = transform.GetChild(0).gameObject;
        _physical.SetActive(false);
        if (_boss is null)
        {
            Debug.Log("No parent boss found");
            Release();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player")){return;}
        GetComponentInParent<Boss>().Aggrevate(other.gameObject);
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
