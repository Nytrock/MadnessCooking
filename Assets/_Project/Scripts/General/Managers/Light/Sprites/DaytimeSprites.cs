using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class DaytimeSprites : DaytimeLight {
        [SerializeField] private Color _spriteColor;

        public Color SpriteColor => _spriteColor;
    }
}
