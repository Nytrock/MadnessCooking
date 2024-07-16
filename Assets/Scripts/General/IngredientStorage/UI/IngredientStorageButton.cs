using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientStorageButton : MonoBehaviour {
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _countText;
    private BuyableItemCount<Ingredient> _countRenderer;

    public void SetVisual(BuyableItemCount<Ingredient> count) {
        _icon.sprite = count.Item.Icon;
        _countRenderer = count;

        UpdateCount(count.Count);
        _countRenderer.CountChanged += UpdateCount;
    }

    private void UpdateCount(int count) {
        _countText.text = count.ToString();
    }
}
