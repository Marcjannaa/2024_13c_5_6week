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
    private bool _movingRight;
    private bool _horizontal;
    //private unsafe float *pos;
    void Start()
    {
        CurrentPos = transform.position;
        _movingRight = InitialDirection.ToLower() == "right";
        StartCoroutine(MoveCoroutine());
        float _pos = 2f;
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
            
            if (_movingRight)
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
            
            if (_movingRight)
            {
                _movingRight = false;
            }
            else
            {
                _movingRight = true;
            }
        }
    }
}
