using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientStorageButton : MonoBehaviour {
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _countText;
    private BuyableItemCount<Ingredient> _countRenderer;
    private int _count;

    public void SetVisual(BuyableItemCount<Ingredient> count) {
        _icon.sprite = count.Item.Icon;
        _countRenderer = count;
    }

    private void Update() {
        if (_countRenderer.Count == _count)
            return;

        _count = _countRenderer.Count;
        _countText.text = _count.ToString();
    }
}
