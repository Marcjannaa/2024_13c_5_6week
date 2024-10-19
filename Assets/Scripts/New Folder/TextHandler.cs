using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text tmp;

    public void DrstroyTxt()
    {
        Destroy(tmp);
    }
}
