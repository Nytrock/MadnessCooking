using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GrayscaleImageRenderer {
    [SerializeField] private Material _grayscaleMaterial;
    [SerializeField] private Image _image;

    public void Setup(GrayscaleImageData data) {
        SetGrayscaleVisibility(data.IsGrayscale);
        SetImage(data.Sprite);
    }

    public void Setup(Sprite sprite, bool isGrayscale) {
        SetGrayscaleVisibility(isGrayscale);
        SetImage(sprite);
    }

    private void SetGrayscaleVisibility(bool isGrayscale) {
        if (isGrayscale)
            _image.material = _grayscaleMaterial;
        else
            _image.material = null;
    }

    private void SetImage(Sprite sprite) {
        if (_image == null)
            return;

        _image.sprite = sprite;
    }

    public void SetActive(bool value) {
        if (_image == null)
            return;

        _image.gameObject.SetActive(value);
    }
}
