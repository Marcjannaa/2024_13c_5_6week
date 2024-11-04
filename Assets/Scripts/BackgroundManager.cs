using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Screen = UnityEngine.Device.Screen;
using Vector3 = UnityEngine.Vector3;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private void Start()
    {
        gameObject.transform.position.Set(
            0,0 ,-10f
        );
        gameObject.transform.SetParent(cam.transform);
        gameObject.transform.position.Set(
            0,0 ,-10f
        );
        AdjustSize();
    }
    private void Update()
    {
        gameObject.transform.position.Set(
            0,0 ,-10f
        );
    }

    void AdjustSize()
    {
        var screenHeight = cam.orthographicSize;
        var screenWidth = screenHeight * cam.aspect;
        transform.localScale = new Vector3(screenWidth, screenHeight, 1);
        
    }
    
}
