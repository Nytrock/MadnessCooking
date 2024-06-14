using TMPro;
using UnityEngine;

public class TextAvailableRenderer : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color _availableColor;
    [SerializeField] private Color _notAvailableColor;

    public void UpdateAvailable(bool isAvailable) {
        if (isAvailable)
            _text.color = _availableColor;
        else
            _text.color = _notAvailableColor;
    }
}
