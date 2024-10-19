using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    [SerializeField] private bool ActiveInGaia;
    [SerializeField] private bool ActiveInNecropolis;
    private Renderer[] renderers;
    private Collider[] colliders;


    public void UpdateObjectState()
    {
        switch (WorldStateManager.WorldState)
        {
            case State.Gaia:
                gameObject.SetActive(ActiveInGaia);
                break;
            case State.Necropolis:
                gameObject.SetActive(ActiveInNecropolis);
                break;
        }
    }
    void Awake()
    {
        WorldStateManager.worldObjects.Add(this);
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
    }
    
    private void SetActiveInGaia()
    {
        bool shouldBeActive = ActiveInGaia;
        foreach (var renderer in renderers)
        {
            renderer.enabled = shouldBeActive;
        }
        foreach (var collider in colliders)
        {
            collider.enabled = shouldBeActive;
        }
    }

    private void SetActiveInNecropolis()
    {
        bool shouldBeActive = ActiveInNecropolis;
        foreach (var renderer in renderers)
        {
            renderer.enabled = shouldBeActive;
        }
        foreach (var collider in colliders)
        {
            collider.enabled = shouldBeActive;
        }
    }
}
