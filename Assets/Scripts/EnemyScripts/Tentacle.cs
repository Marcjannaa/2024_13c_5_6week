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
    
    //do poprawki, nie rusza się głupia
public IEnumerator Sweep(string direction)
{
    Vector3 defaultPosition = transform.position;
    Quaternion defaultRotation = transform.rotation;

    Vector3 newPosition = defaultPosition;
    float angle = 0f;
    
    switch (direction)
    {
        case "left":
            newPosition = new Vector3(defaultPosition.x + 0.6f, defaultPosition.y + 0.43f, defaultPosition.z);
            angle = -33f;
            break;
        case "right":
            newPosition = new Vector3(defaultPosition.x - 0.6f, defaultPosition.y + 0.43f, defaultPosition.z);
            angle = 33f;
            break;
    }

    float chargeDuration = 2f;
    float attackDuration = 1f;

    float elapsedTime = 0f;
    
    while (elapsedTime < chargeDuration)
    {
        transform.position = Vector3.Lerp(defaultPosition, newPosition, elapsedTime / chargeDuration);
        
        float rotation = Mathf.Lerp(0, angle, elapsedTime / chargeDuration);
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        
        elapsedTime += Time.deltaTime;
        
        yield return null;
    }
    
    transform.position = newPosition;
    transform.rotation = Quaternion.Euler(0, 0, angle);
    
    elapsedTime = 0f;
    
    while (elapsedTime < attackDuration)
    {
        transform.position = Vector3.Lerp(newPosition, defaultPosition, elapsedTime / attackDuration);
        
        float rotation = Mathf.Lerp(angle, 0, elapsedTime / attackDuration);
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        
        elapsedTime += Time.deltaTime;
        
        yield return null;
    }
    
    transform.position = defaultPosition;
    transform.rotation = defaultRotation;
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

