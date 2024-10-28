using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider, effectsSlider;
    [SerializeField] private TMP_Text jumpBinding, rightBinding, leftBinding, dashBinding, attackBinding;

    public void OnMusicSliderChanged() { }

    public void OnEffectsSliderChanged() { }
    
    public void OnExitClicked() { SceneManager.LoadScene("MainMenu"); }
    
    public void OnJumpBindigClicked() { StartCoroutine(GetAnyButton(jumpBinding)); }
    
    public void OnLeftBindigClicked() { StartCoroutine(GetAnyButton(leftBinding)); }
    
    public void OnRightBindigClicked() { StartCoroutine(GetAnyButton(rightBinding)); }
    
    public void OnDashBindigClicked() { StartCoroutine(GetAnyButton(dashBinding)); }
    
    public void OnAttackBindingClicked() { StartCoroutine(GetAnyButton(attackBinding)); }
    
    private IEnumerator GetAnyButton(TMP_Text tmpText)
    {
        yield return new WaitUntil( () => !Input.GetKey(KeyCode.None));
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            if (Input.GetKey(kcode))
                tmpText.text = kcode.ToString();
    }
}
