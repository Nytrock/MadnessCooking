using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class ImageStyleChanger<TValue> : UIStyleChanger<TValue, Sprite> {
    protected Image _image;

    protected override void SetStyle(Sprite sprite) {
        if (_image == null)
            GetImage();

        _image.sprite = sprite;
    }

    private void GetImage() {
        _image = GetComponent<Image>();
    }
}
