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
                break;
            case State.Necropolis:
                WorldState = State.Gaia;
                break;
        }

        UpdateAllObjects();
    }
    private void UpdateAllObjects()
    {
        foreach (WorldObject obj in worldObjects)
        {
            obj.UpdateObjectState();
        }
    }
    
}
