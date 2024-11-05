using System;
using UnityEngine;

public class StuckDetector : MonoBehaviour
{
    private Vector2 _currPos;
    private Vector2 _prevPos;
    private float _refreshTimer=0;
    private float _refreshInterval=0.2f;
    private float _error = 0.1f;

    private void Start()
    {
        Clear();
    }

    private void Update()
    {
        if (_refreshTimer < _refreshInterval)
        {
            _refreshTimer += Time.deltaTime;
        }
        else
        {
            _prevPos = _currPos;
            _currPos = transform.position;
        }
    }

    public void Clear()
    {
        _currPos = transform.position;
        _prevPos = new Vector2(_currPos.x + 2 * _error, _currPos.y + 2 * _error);
    }

    public bool IsStuck()
    {
        return IsStuckX() && IsStuckY();
    }

    public bool IsStuckX()
    {
        return  Math.Abs(_currPos.x - _prevPos.x) < _error;
    }
    public bool IsStuckY()
    {
        return Math.Abs(_currPos.y - _prevPos.y) < _error;
    }

    public void HoldUp(float time)
    {
        _refreshTimer -= time;
    }
    public void HoldUp()
    {
        HoldUp(2*_refreshInterval);
    }
}
