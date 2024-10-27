using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tentacle : MonoBehaviour
{
    [SerializeField] private GameObject tentacleProjectile;
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    public void Shoot(GameObject player)
    {
        Collider2D collider = GetComponent<Collider2D>();
        Bounds bounds = collider.bounds;
        Vector3 pos = new Vector3(bounds.center.x, bounds.min.y, 0);
        GameObject projectile = Instantiate(tentacleProjectile,pos,tentacleProjectile.transform.rotation);
        projectile.GetComponent<TentacleProjectile>().Initialize(player);
    }

    public void Rotate(int dir)
    {
        Vector3 pivotOffset = new Vector3(-transform.localScale.x / 2, 0, 0);
        Vector3 rotationAngle = new Vector3(0,0,0);
        switch (dir)
        {
            case 0:
                StartCoroutine(RotateOverTime(-5, 1));
                break;
            case 1:
                StartCoroutine(RotateOverTime(5, 1));
                break;
        }
    }
    
    private IEnumerator RotateOverTime(float rotationSpeed, float duration)
    {
        float elapsedTime = 0f;
        
        float totalRotation = rotationSpeed * duration;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            
            float angle = Mathf.Lerp(0, totalRotation, elapsedTime / duration);
            
            transform.rotation = Quaternion.Euler(0, 0, angle);

            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, totalRotation);
    }
    
    public IEnumerator ReturnToInitialRotation()
    {
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = initialRotation;
    }
}

