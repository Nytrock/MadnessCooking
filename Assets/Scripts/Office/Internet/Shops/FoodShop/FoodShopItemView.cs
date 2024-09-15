using UnityEngine;

public class FoodShopItemView : BaseChooseShopItemView<Food> {
    [SerializeField] private FoodShopRecipe _recipeRenderer;
    [SerializeField] private HoverItemName _hoverText;

    protected override void Start() {
        base.Start();
        _recipeRenderer.SetHoverText(_hoverText);
    }

    public void SetUpgradeDataToRecipe(KitchenUpgradeData upgradeData) {
        _recipeRenderer.SetUpgradeData(upgradeData);
    }

    protected override void SetInfo() {
        base.SetInfo();
        _recipeRenderer.SetupRecipe(_selectedItem);
    }

    public override void ResetInfo() {
        base.ResetInfo();
        _recipeRenderer.DisableParts();
    }
}
