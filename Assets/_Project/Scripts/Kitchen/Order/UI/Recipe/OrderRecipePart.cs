using UnityEngine;

public class OrderRecipePart : FoodRecipePart {
    [SerializeField] private TextAvailableRenderer _countTextRenderer;
    [SerializeField] private Sprite _moneySprite;
    private BuyableItemCount<Ingredient> _ingredientCount;

    public BuyableItemCount<Ingredient> IngredientCount => _ingredientCount;

    public override void Setup(BuyableItemCount<Ingredient> count, bool isAvailable) {
        base.Setup(count, isAvailable);
        _countTextRenderer.UpdateAvailable(isAvailable);
        _ingredientCount = count;
    }

    public void SetupAutoSpice(BuyableItemCount<Ingredient> ingredientCount) {
        gameObject.SetActive(true);

        _ingredientCount = ingredientCount;
        _showingItem = ConstIngredients.Instance.Money;
        UpdateAutoSpiceStatus(MoneyManager.Instance.MoneyCount);
    }

    public void UpdateAutoSpiceStatus(int moneyCount) {
        int price = _ingredientCount.Count * _ingredientCount.Item.Price;
        bool isMoneyEnough = moneyCount >= price;
        _isAvailable = isMoneyEnough;

        _countText.text = price.ToString() + "x";
        _grayscaleIcon.Setup(_moneySprite, !isMoneyEnough);
        _countTextRenderer.UpdateAvailable(isMoneyEnough);
    }

    public override void Setup(Technic technic, bool isAvailable) {
        base.Setup(technic, isAvailable);
        _countTextRenderer.UpdateAvailable(isAvailable);
    }

    public void UpdateAvailable(bool isAvailable) {
        _isAvailable = isAvailable;
        _countTextRenderer.UpdateAvailable(isAvailable);
        _grayscaleIcon.SetGrayscaleVisibility(!isAvailable);
    }
}
