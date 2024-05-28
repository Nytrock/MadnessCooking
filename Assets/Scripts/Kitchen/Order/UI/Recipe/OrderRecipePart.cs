using UnityEngine;

public class OrderRecipePart : ShopFoodRecipePart {
    [SerializeField] private Sprite _moneySprite;
    [SerializeField] private Color _availableColor;
    [SerializeField] private Color _notAvailableColor;

    public override void Setup(IngredientCount count, bool isAvailable) {
        base.Setup(count, isAvailable);
        if (isAvailable)
            _countText.color = _availableColor;
        else
            _countText.color = _notAvailableColor;
    }

    public void SetupAutoSpice(IngredientCount ingredientCount) {
        gameObject.SetActive(true);
        _icon.sprite = _moneySprite;
        int price = ingredientCount.Count * ConstIngredients.Instance.Spice.Price;

        _countText.text = price.ToString() + "x";
        if (MoneyManager.Instance.MoneyCount >= price)
            _countText.color = _availableColor;
        else
            _countText.color = _notAvailableColor;
    }

    public override void Setup(Technic technic, bool isFree) {
        base.Setup(technic, isFree);
        if (isFree)
            _countText.color = _availableColor;
        else
            _countText.color = _notAvailableColor;
    }
}
