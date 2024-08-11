using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PopularityUIPart {
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;

    public void SetSprite(int index) {
        if (index < 0 || index >= _sprites.Length)
            return;

        _image.sprite = _sprites[index];
    }
}
