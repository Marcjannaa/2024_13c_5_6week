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
    private bool working;
    void Start()
    {
        CurrentPos = transform.position;
        MovingRight = InitialDirection.ToLower() == "right";

        StartCoroutine(MoveCoroutine());
        working = true;
=======
    [SerializeField] private bool lift;
    private Vector3 _currentPos;
    private bool _moving;
    private bool _horizontal;
    
    private void Start()
    {
        _currentPos = transform.position;
        _moving = InitialDirection.ToLower() == "right" || InitialDirection.ToLower() == "down";
        _horizontal = InitialDirection.ToLower() != "up" && InitialDirection.ToLower() != "down";
        if (!lift) StartCoroutine(MoveCoroutine());

    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player") ||
            (other.gameObject.transform.parent != null))
        { return; }
        other.gameObject.transform.SetParent(gameObject.transform);
        _moving = !_moving;
        if (lift) StartCoroutine(MoveCoroutine());
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.transform.parent == gameObject.transform)
            other.gameObject.transform.SetParent(null);
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            float target;
            _currentPos = transform.position;
            if (_moving) target = (_horizontal ? _currentPos.x : _currentPos.y) + MovingRange;
            else target = (_horizontal ? _currentPos.x : _currentPos.y) - MovingRange;
            var targetPos = _horizontal ? new Vector3(target, transform.position.y, transform.position.z) : new Vector3(transform.position.x, target, transform.position.z);
            while (Mathf.Abs(_horizontal ? transform.position.x - target : transform.position.y - target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, MovingSpeed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(MoveDelay);
            
            print(_moving);
            if (lift) break;
            _moving = !_moving;
        }
    }
}
