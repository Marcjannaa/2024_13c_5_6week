using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class PlayerStash : MonoBehaviour
{
    private int _rosesCount, _keysCount, _soulsCount = 0;
    public enum GainedItem
    {
        Roses, Souls, Keys
    }
    public void Add(GainedItem item, int count=1)
    {
        switch (item)
        {
            case GainedItem.Roses:
                _rosesCount += count;
                break;
            case GainedItem.Keys:
                _keysCount += count;
                break;
            case GainedItem.Souls:
                _soulsCount += count;
                break;
            default: break;
        }
    }

    public int GetRoses() { return _rosesCount; }
    
    public int GetKeys() { return _keysCount; }
    
    public int GetSouls() { return _soulsCount; } 
    
}
