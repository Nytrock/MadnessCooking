using UnityEngine;
using UnityEngine.UI;

namespace MadnessCooking.General {
    public class GrayscaleImage : Image {
        private Material _grayscaleMaterial;

        public void Setup(GrayscaleImageData data) {
            Setup(data.Sprite, data.IsGrayscale);
        }

        public void Setup(Sprite sprite, bool isGrayscale) {
            SetGrayscaleVisibility(isGrayscale);
            SetSprite(sprite);
            SetActive(true);
        }

        public void SetGrayscaleVisibility(bool isGrayscale) {
            if (_grayscaleMaterial == null)
                _grayscaleMaterial = MaterialManager.Instance.GrayscaleMaterial;

            if (isGrayscale)
                material = _grayscaleMaterial;
            else
                material = null;
        }

        private void SetSprite(Sprite _sprite) {
            sprite = _sprite;
        }

        public void SetActive(bool value) {
            gameObject.SetActive(value);
        }
    }
}
