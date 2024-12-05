using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ClueHole : MonoBehaviour {
    [SerializeField] private Image _raycastBlocker;
    [SerializeField] private Image _background;
    private Image _image;

    private void Awake() {
        GetImage();
    }

    private void Update() {
        _raycastBlocker.raycastTarget = !_image.rectTransform.ContainsCamera();
    }

    public void ChangeTransform(ClueTemplate template) {
        if (_image == null)
            GetImage();

        _image.rectTransform.sizeDelta = template.RectTransform.sizeDelta;
        _image.rectTransform.position = template.RectTransform.position;
        _image.raycastTarget = template.IsHoleBlocked;

        _background.transform.localPosition = _image.rectTransform.localPosition * new Vector2(-1, -1);
    }

    private void GetImage() {
        if (_image != null)
            return;

        _image = GetComponent<Image>();
    }
}
