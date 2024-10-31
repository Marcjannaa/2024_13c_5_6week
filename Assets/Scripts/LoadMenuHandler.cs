using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnExitClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
