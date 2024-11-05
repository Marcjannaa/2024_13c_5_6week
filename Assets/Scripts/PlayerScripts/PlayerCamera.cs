using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector3 _offset = new Vector3(0f, 0f, -30f);

    private float _time = 0.25f;

    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void Start()
    {
        gameObject.transform.position.Set(target.position.x, target.position.y, _offset.z);
    }

    void Update()
    {
        if (target == null){return;}
        var targetPos = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, _time);
    }
}
