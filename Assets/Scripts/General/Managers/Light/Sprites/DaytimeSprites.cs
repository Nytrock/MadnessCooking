using System;
using UnityEngine;

[Serializable]
public class DaytimeSprites : DaytimeLight {
    [SerializeField] private Color _spriteColor;

    public Color SpriteColor => _spriteColor;
}
