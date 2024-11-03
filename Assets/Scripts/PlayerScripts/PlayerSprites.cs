using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSprites : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    [SerializeField] private Animation anim;

    public void LookLeft(bool looksToLeft)
    {
        var theScale = transform.localScale;
        theScale.x = looksToLeft ? -1f : 1f;
        transform.localScale = theScale;
    }
}
