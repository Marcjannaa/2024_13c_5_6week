using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private string InitialDirection;
    [SerializeField] private float MovingSpeed;
    [SerializeField] private float MovingRange;
    [SerializeField] private float MoveDelay;
    private Vector3 CurrentPos;
    private bool MovingRight;
    void Start()
    {
        CurrentPos = transform.position;
        MovingRight = InitialDirection.ToLower() == "right";
        StartCoroutine(MoveCoroutine());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.transform.SetParent(gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.transform.SetParent(null);
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            float targetX;
            CurrentPos = transform.position;
            
            if (MovingRight)
            {
                targetX = CurrentPos.x + MovingRange;
            }
            else
            {
                targetX = CurrentPos.x - MovingRange;
            }

            Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
            
            while (Mathf.Abs(transform.position.x - targetX) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, MovingSpeed * Time.deltaTime);
                yield return null;
            }
            
            yield return new WaitForSeconds(MoveDelay);
            
            if (MovingRight)
            {
                MovingRight = false;
            }
            else
            {
                MovingRight = true;
            }
        }
    }
}
