using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BuyableItemText : MonoBehaviour {
    private BuyableItemTextType _type;
    private BuyableItem _nowItem;
    private TextMeshProUGUI _text;

    private void Awake() {
        GetText();
    }

    private void Start() {
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

    private void UpdateText() {
        if (_text == null)
            GetText();

        if (_nowItem == null) {
            _text.text = "";
            return;
        }

        if (_type == BuyableItemTextType.Name)
            _text.text = _nowItem.Name;
        else
            _text.text = _nowItem.Description;
    }

    private void GetText() {
        _text = GetComponent<TextMeshProUGUI>();
    }
}
