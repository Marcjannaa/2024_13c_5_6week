using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    private enum Action {Jump, Right, Left, Dash, Attack};

    [SerializeField] private Slider musicSlider, effectsSlider;
    [SerializeField] private TMP_Text jumpBinding, rightBinding, leftBinding, dashBinding, attackBinding;
    private TMP_Text _tmpText;
    //private InputManagerEntry.Axis _axis;
    public void OnMusicSliderChanged() { }

    public void OnEffectsSliderChanged() { }
    
    public void OnExitClicked() { SceneManager.LoadScene("MainMenu"); }
    
    public void OnJumpBindigClicked() { StartCoroutine(GetAnyButton(Action.Jump)); }
    
    public void OnLeftBindigClicked() { StartCoroutine(GetAnyButton(Action.Left)); }
    
    public void OnRightBindigClicked() { StartCoroutine(GetAnyButton(Action.Right)); }
    
    public void OnDashBindigClicked() { StartCoroutine(GetAnyButton(Action.Dash)); }
    
    public void OnAttackBindingClicked() { StartCoroutine(GetAnyButton(Action.Attack)); }
    
    private IEnumerator GetAnyButton(Action action)
    {
        switch (action)
        {
            case Action.Attack:
                _tmpText = attackBinding;
                break;
            case Action.Right:
                _tmpText = rightBinding;
                break;
            case Action.Left:
                _tmpText = leftBinding;
                break;
            case Action.Dash:
                _tmpText = dashBinding;
                break;
            case Action.Jump:
                _tmpText = jumpBinding;
                break;
        }
        yield return new WaitUntil( () => !Input.GetKey(KeyCode.None));
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            if (Input.GetKey(kcode))
                _tmpText.text = kcode.ToString();
            
    }
}
