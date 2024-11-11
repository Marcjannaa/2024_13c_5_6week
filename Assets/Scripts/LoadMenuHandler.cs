using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuHandler : MonoBehaviour
{
    public static int slotNum;
    
    public static void SaveToCurrentSlot()
    {
        PlayerPrefs.SetInt("Roses"+ slotNum, PlayerPrefs.GetInt("Roses"));
        PlayerPrefs.SetInt("Souls"+ slotNum, PlayerPrefs.GetInt("Souls"));
        PlayerPrefs.SetFloat("MeleeAttack"+ slotNum, PlayerPrefs.GetFloat("MeleeAttack"));
        PlayerPrefs.SetFloat("MaxHp"+ slotNum, PlayerPrefs.GetFloat("MaxHp"));
        PlayerPrefs.SetFloat("DashDuration"+ slotNum, PlayerPrefs.GetFloat("DashDuration"));
        PlayerPrefs.SetFloat("Stamina"+ slotNum, PlayerPrefs.GetFloat("Stamina"));
        PlayerPrefs.SetInt("CanSwim" + slotNum, PlayerPrefs.GetInt("CanSwim"));
        PlayerPrefs.Save();
    }
    public void OnSlotClicked(string slotNum)
    {
        if (MenuManager.newGame)
        {
            SceneManager.LoadScene("Tutorial");
            PlayerPrefs.SetInt("Roses" + slotNum, 0);
            PlayerPrefs.SetInt("Souls"+ slotNum, 0);
            PlayerPrefs.SetFloat("MeleeAttack"+ slotNum, 10f);
            PlayerPrefs.SetFloat("MaxHp"+ slotNum, 100f);
            PlayerPrefs.SetFloat("DashDuration"+ slotNum, 1f);
            PlayerPrefs.SetFloat("Stamina"+ slotNum, 0.3f);
            PlayerPrefs.SetInt("CanSwim" + slotNum, 0);
            PlayerPrefs.Save();
        }
        else SceneManager.LoadScene(PlayerPrefs.GetString("LastScene" + slotNum));
        PlayerPrefs.SetInt("Roses", PlayerPrefs.GetInt("Roses" + slotNum));
        PlayerPrefs.SetInt("Souls", PlayerPrefs.GetInt("Souls" + slotNum));
        PlayerPrefs.SetFloat("MeleeAttack", PlayerPrefs.GetFloat("MeleeAttack"+slotNum));
        PlayerPrefs.SetFloat("MaxHp", PlayerPrefs.GetFloat("MaxHp" + slotNum));
        PlayerPrefs.SetFloat("DashDuration", PlayerPrefs.GetFloat("DashDuration" + slotNum));
        PlayerPrefs.SetFloat("Stamina", PlayerPrefs.GetFloat("Stamina" + slotNum));
        PlayerPrefs.SetInt("CanSwim", PlayerPrefs.GetInt("CanSwim" + slotNum));
        PlayerPrefs.Save();
        LoadMenuHandler.slotNum = int.Parse(slotNum);
    }
    public void OnExitClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
