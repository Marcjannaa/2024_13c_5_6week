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
    void Start()
    {
        gameObject.transform.SetParent(cam.transform);
        AdjustSize();
    }

    private void Update()
    {
        //AdjustSize();
    }

    void AdjustSize()
    {
        float screenHeight = cam.orthographicSize * 2;
        float screenWidth = screenHeight * cam.aspect;
        transform.localScale = new Vector3(screenWidth/8, screenHeight/7, 1);
    }
    
}
