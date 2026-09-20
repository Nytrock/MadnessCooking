using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class GrayscaleImageData {
        private readonly Sprite _sprite;
        private readonly bool _isGrayscale;

        public Sprite Sprite => _sprite;
        public bool IsGrayscale => _isGrayscale;

        public GrayscaleImageData(Sprite sprite, bool isGrayscale) {
            _sprite = sprite;
            _isGrayscale = isGrayscale;
        }
    }
}
