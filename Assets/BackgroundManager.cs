using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Screen = UnityEngine.Device.Screen;
using Vector3 = UnityEngine.Vector3;

public class BackgroundManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        //gameObject.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        gameObject.GetComponentInParent<Canvas>().sortingLayerID = 0;
        gameObject.GetComponentInParent<Canvas>().sortingOrder = 0;
        gameObject.GetComponentInParent<Canvas>().sortingLayerName = "Default";
    }
}
