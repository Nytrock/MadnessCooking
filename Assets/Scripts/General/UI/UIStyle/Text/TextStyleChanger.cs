using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextStyleChanger<TValue> : UIStyleChanger<TValue, Color> {
    private TextMeshProUGUI _text;

    protected override void SetStyle(Color color) {
        if (_text == null)
            GetText();

        _text.color = color;
    }

    private void GetText() {
        _text = GetComponent<TextMeshProUGUI>();
    }
}
