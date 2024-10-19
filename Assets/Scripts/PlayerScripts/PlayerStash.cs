using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class PlayerStash : MonoBehaviour
{
    private int _rosesCount, _keysCount, _soulsCount = 0;
    public enum Item { Roses, Souls, Keys }
    public virtual void Add(Item item, int count=1)
    {
        switch (item)
        {
            case Item.Roses:
                _rosesCount += count;
                GetComponent<PlayerStats>().UpdateUI();
                break;
            case Item.Keys:
                _keysCount += count;
                GetComponent<PlayerStats>().UpdateUI();
                break;
            case Item.Souls:
                _soulsCount += count;
                GetComponent<PlayerStats>().UpdateUI();
                break;
            default: break;
        }
    }

    public int GetRoses() { return _rosesCount; }
    
    public int GetKeys() { return _keysCount; }
    
    public int GetSouls() { return _soulsCount; } 
    
}
