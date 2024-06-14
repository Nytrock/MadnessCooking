using System;
using UnityEngine;

[Serializable]
public class GrayscaleImageData {
    private Sprite _sprite;
    private bool _isGrayscale;

    public Sprite Sprite => _sprite;
    public bool IsGrayscale => _isGrayscale;

    public GrayscaleImageData(Sprite sprite, bool isGrayscale) {
        _sprite = sprite;
        _isGrayscale = isGrayscale;
    }
}
