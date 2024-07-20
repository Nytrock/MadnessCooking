using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GrayscaleImageRenderer {
    [SerializeField] private Image _image;
    private Material _grayscaleMaterial;

    public void Setup(GrayscaleImageData data) {
        SetGrayscaleVisibility(data.IsGrayscale);
        SetSprite(data.Sprite);
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
            _image.material = _grayscaleMaterial;
        else
            _image.material = null;
    }

    public void SetImage(Image image) {
        _image = image;
    }

    private void SetSprite(Sprite sprite) {
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
