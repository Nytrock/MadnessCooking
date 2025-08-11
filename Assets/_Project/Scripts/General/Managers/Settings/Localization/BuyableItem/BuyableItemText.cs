using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BuyableItemText : MonoBehaviour {
    private BuyableItemTextType _type;
    private BuyableItem _nowItem;
    private bool _isHidden;
    private TextMeshProUGUI _text;

    private void Start() {
        UpdateText();
        LocalizationManager.Instance.LocalizationChanged += UpdateText;
    }

    public void SetType(BuyableItemTextType type) {
        _type = type;
    }

    public void SetItem(BuyableItem item) {
        _nowItem = item;
        UpdateText();
    }

    public void ResetItem() {
        _nowItem = null;
        UpdateText();
    }

    private async void UpdateText() {
        CheckText();
        if (_nowItem == null) {
            _text.text = "";
            return;
        }

        if (_isHidden) {
            _text.text = "???";
            return;
        }

        if (_type == BuyableItemTextType.Name)
            _text.text = await _nowItem.GetName();
        else
            _text.text = await _nowItem.GetDescription();
    }

    private void CheckText() {
        if (_text != null) return;

        _text = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeHiddenState(bool isHidden) {
        _isHidden = isHidden;
    }
}
