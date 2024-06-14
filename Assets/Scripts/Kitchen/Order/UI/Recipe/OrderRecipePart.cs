using UnityEngine;

public class OrderRecipePart : FoodRecipePart {
    [SerializeField] private Sprite _moneySprite;
    [SerializeField] private TextAvailableRenderer _countTextRenderer;

    public override void Setup(IngredientCount count, bool isAvailable) {
        base.Setup(count, isAvailable);
        _countTextRenderer.UpdateAvailable(isAvailable);
    }

    public void SetupAutoSpice(IngredientCount ingredientCount) {
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
}
