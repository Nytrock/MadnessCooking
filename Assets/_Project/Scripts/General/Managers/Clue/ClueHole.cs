using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ClueHole : MonoBehaviour {
    [SerializeField] private Image _cutout;
    private Image _mask;

    private void Awake() {
        GetImage();
    }

    public void ChangeTransform(ClueTemplate template) {
        if (_mask == null)
            GetImage();

        _mask.rectTransform.sizeDelta = template.RectTransform.sizeDelta;
        _mask.rectTransform.position = template.RectTransform.position;
        _mask.raycastTarget = template.IsHoleBlocked;
    }

    private void GetImage() {
        if (_mask != null)
            return;

        _mask = GetComponent<Image>();
    }
}
