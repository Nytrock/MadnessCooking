using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextAvailableRenderer : MonoBehaviour {
    [SerializeField] private Color _availableColor;
    [SerializeField] private Color _notAvailableColor;
    private TextMeshProUGUI _text;

    private void Awake() {
        GetTextMesh();
    }

    private void GetTextMesh() {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateAvailable(bool isAvailable) {
        if (_text == null)
            GetTextMesh();

        if (isAvailable)
            _text.color = _availableColor;
        else
            _text.color = _notAvailableColor;
    }
}
