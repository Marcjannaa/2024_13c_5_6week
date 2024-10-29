using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprites : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite left, right;

    public void LookLeft(bool looksToLeft)
    {
        _spriteRenderer.sprite = looksToLeft ? left : right;
    }
}
