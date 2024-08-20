using UnityEngine;
using UnityEngine.UI;

public class IngredientChoiceRenderer : MonoBehaviour {
    [SerializeField] private IngredientChoiceStyle[] _styles;
    [SerializeField] private Image _panel;
    [SerializeField] private Image _close;
    [SerializeField] private Image _submitButton;
    [SerializeField] private LocalizedText _submitText;

    private IngredientChoiceStyle _nowStyle;

    public void UpdateStyle(BedType bedType) {
        foreach (var style in _styles) {
            if (style.BedType == bedType) {
                SetStyle(style);
                return;
            }
        }

        SetStyle(_styles[0]);
    }

    public void SetButtonStyle(IngredientChoiceButton choiceButton) {
        choiceButton.SetButtonImage(_nowStyle.Cell);
    }

    private void SetStyle(IngredientChoiceStyle style) {
        _nowStyle = style;
        _panel.sprite = _nowStyle.Panel;
        _close.sprite = _nowStyle.Close;
        _submitButton.sprite = _nowStyle.Button;
        _submitText.SetColor(_nowStyle.Color);
    }
}
