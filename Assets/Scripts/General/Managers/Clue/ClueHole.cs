using UnityEngine;
using UnityEngine.UI;

public class ClueHole : MonoBehaviour {
    [SerializeField] private Image _background;
    private Image _image;

    private void Awake() {
        GetImage();
    }

    private void Update() {
        Vector2 localMousePosition = _image.rectTransform.InverseTransformPoint(Input.mousePosition);
        _background.raycastTarget = !_image.rectTransform.rect.Contains(localMousePosition);
    }

    public void ChangeTransform(ClueTemplate template) {
        if (_image == null)
            GetImage();

        _image.rectTransform.sizeDelta = template.RectTransform.sizeDelta;
        _image.rectTransform.position = template.RectTransform.position;
        _image.raycastTarget = template.IsScreeenBlocked;

        _background.transform.localPosition = _image.rectTransform.localPosition * new Vector2(-1, -1);
    }

    private void GetImage() {
        if (_image != null)
            return;

        _image = GetComponent<Image>();
    }
}
