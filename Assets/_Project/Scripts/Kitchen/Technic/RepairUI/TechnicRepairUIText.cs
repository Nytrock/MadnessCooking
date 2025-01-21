using UnityEngine;

[RequireComponent(typeof(LocalizedText), typeof(TextAvailableRenderer))]
public class TechnicRepairUIText : MonoBehaviour {
    [SerializeField] private Color _defaultColor;
    [SerializeField] private string _fullStrengthMessage;

    private LocalizedText _localizedText;
    private TextAvailableRenderer _textAvailableRenderer;

    private void Awake() {
        _localizedText = GetComponent<LocalizedText>();
        _textAvailableRenderer = GetComponent<TextAvailableRenderer>();
    }

    private void SetFullStrengthMessage() {
        _localizedText.SetColor(_defaultColor);
        _localizedText.SetText(_fullStrengthMessage);
    }

    public void SetPrice(int price) {
        if (price == 0) {
            SetFullStrengthMessage();
            return;
        }

        _textAvailableRenderer.UpdateAvailable(price <= MoneyManager.Instance.MoneyCount);
        _localizedText.SetText(price.ToString() + '$');
    }
}
