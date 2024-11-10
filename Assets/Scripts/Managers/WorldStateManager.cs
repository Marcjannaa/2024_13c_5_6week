using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum State
{
    Gaia,
    Necropolis
}
public class WorldStateManager : MonoBehaviour
{
    public static List<WorldObject> worldObjects = new List<WorldObject>();

    private State previousState;
    public static State WorldState;
    void Start()
    {
        WorldState = State.Necropolis;
        UpdateAllObjects();
    }

    public void ChangeState()
    {
        switch (WorldState)
        {
            case State.Gaia:
                WorldState = State.Necropolis;
                Debug.Log("necro");
                break;
            case State.Necropolis:
                WorldState = State.Gaia;
                Debug.Log("gaia");
                break;
        }
        UpdateAllObjects();
    }
    private void UpdateAllObjects()
    {
        List<WorldObject> toRemove = new List<WorldObject>();
        foreach (WorldObject obj in worldObjects)
        {
            try
            {
                obj.UpdateObjectState();
            }
            catch(MissingReferenceException e)
            {
               toRemove.Add(obj);
            }
        }

        foreach (WorldObject obj in toRemove)
        {
            worldObjects.Remove(obj);
        }
    }
    
}
