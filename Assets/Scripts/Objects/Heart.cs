using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public void ChangeHeartCount(bool val)
    {
        gameObject.SetActive(val);
    }
}
