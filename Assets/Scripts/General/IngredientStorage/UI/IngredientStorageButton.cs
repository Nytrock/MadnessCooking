using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientStorageButton : MonoBehaviour {
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _countText;
    private IngredientCount _countRenderer;
    private int _count;

    public void SetVisual(IngredientCount count) {
        _icon.sprite = count.Ingredient.Icon;
        _countRenderer = count;
    }

    private void Update() {
        if (_countRenderer.Count == _count)
            return;

        _count = _countRenderer.Count;
        _countText.text = _count.ToString();
    }
}
