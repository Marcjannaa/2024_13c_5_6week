using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class ColorMode : MonoBehaviour
{
    [SerializeField] private Light2D ld;
    private void Start()
    {
        ld.color = new Color(255, 23, 35, 255);
    }
}
