using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class DaytimeRenderInfo {
        [SerializeField] private Daytime _daytime;
        [SerializeField] private Sprite _iconSprite;
        [SerializeField] private Color _textColor;

        public Daytime Daytime => _daytime;
        public Sprite Icon => _iconSprite;
        public Color Color => _textColor;
    }
}
