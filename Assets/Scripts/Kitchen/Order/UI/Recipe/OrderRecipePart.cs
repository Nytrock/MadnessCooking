using UnityEngine;

public class OrderRecipePart : FoodRecipePart {
    [SerializeField] private Sprite _moneySprite;
    [SerializeField] private TextAvailableRenderer _countTextRenderer;
    private BuyableItemCount<Ingredient> _ingredientCount;

    public BuyableItemCount<Ingredient> IngredientCount => _ingredientCount;

    public override void Setup(BuyableItemCount<Ingredient> count, bool isAvailable) {
        base.Setup(count, isAvailable);
        _countTextRenderer.UpdateAvailable(isAvailable);
        _ingredientCount = count;
    }

    public void SetupAutoSpice(BuyableItemCount<Ingredient> ingredientCount) {
        gameObject.SetActive(true);
        int price = ingredientCount.Count * ConstIngredients.Instance.Spice.Price;
        bool isMoneyEnough = MoneyManager.Instance.MoneyCount >= price;

        _countText.text = price.ToString() + "x";
        _icon.Setup(_moneySprite, isMoneyEnough);
        _countTextRenderer.UpdateAvailable(isMoneyEnough);
    }

    public override void Setup(Technic technic, bool isAvailable) {
        base.Setup(technic, isAvailable);
        _countTextRenderer.UpdateAvailable(isAvailable);
    }

    public void UpdateAvailable(bool isAvailable) {
        _countTextRenderer.UpdateAvailable(isAvailable);
    }
}
