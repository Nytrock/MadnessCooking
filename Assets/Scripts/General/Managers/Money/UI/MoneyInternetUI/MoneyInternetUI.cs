using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MoneyInternetUI : MoneyBaseUI {
    private TextMeshProUGUI _countText;

    protected override void Awake() {
        base.Awake();
        _countText = GetComponent<TextMeshProUGUI>();
    }

    protected override void StartAnimation() {
        _countText.text = _countConverted;
    }
}
