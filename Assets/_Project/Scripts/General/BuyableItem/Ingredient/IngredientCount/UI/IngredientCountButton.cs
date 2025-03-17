using TMPro;
using UnityEngine;

public class IngredientCountButton : HoverItemNameActivator {
    [SerializeField] private TextMeshProUGUI _countText;
    private IngredientCount _countRenderer;

    public IngredientCount IngredientCount => _countRenderer;

    public void SetVisual(IngredientCount count) {
        _icon.sprite = count.Ingredient.Icon;
        _showingItem = count.Ingredient;
        _countRenderer = count;

        UpdateCount(count.Count);
        _countRenderer.CountChanged += UpdateCount;
    }

    public void ResetVisual() {
        _countRenderer.CountChanged -= UpdateCount;
        _showingItem = null;
        _countRenderer = null;
    }

    private void UpdateCount(int count) {
        _countText.text = count.ToString();
    }
}
