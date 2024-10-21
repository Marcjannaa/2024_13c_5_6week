using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdateBar(float currentHp, float maxHp)
    {
        slider.value = currentHp / maxHp;
    }
}
