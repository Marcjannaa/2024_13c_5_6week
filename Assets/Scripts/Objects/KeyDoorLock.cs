using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorLock : MonoBehaviour
{
    [SerializeField] private GameObject door;

    [SerializeField] private int keyCountRequired = 0;
    [SerializeField] private int pulses=2;
    [SerializeField] private int colorStep = 2;

    private int _currentColor = 255;
    private int _pulsesDone = 0;
    

    private SpriteRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _pulsesDone = pulses;
    }

    // Update is called once per frame
    void Update()
    {
        if (_pulsesDone<pulses)
        {
            _currentColor-=colorStep;
            if (_currentColor < 0)
            {
                _pulsesDone++;
                _currentColor = 255;
            }
            _renderer.color = new Color(255, _currentColor, _currentColor);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) {return;}
        if (PlayerPrefs.GetInt("Keys") < keyCountRequired)
        {
            _pulsesDone = 0;
        }
        else
        {
            Destroy(door);
            Destroy(gameObject);
        }
    }
}
