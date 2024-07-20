using UnityEngine;

public class FoodShopItemView : BaseChooseShopItemView<Food> {
    [SerializeField] private FoodShopRecipe _recipeRenderer;

    public void SetUpgradeDataToRecipe(KitchenUpgradeData upgradeData) {
        _recipeRenderer.SetUpgradeData(upgradeData);
    }

    protected override void SetInfo(Food item) {
        base.SetInfo(item);
        _recipeRenderer.SetupRecipe(item);
    }

    public override void ResetInfo() {
        base.ResetInfo();
        _recipeRenderer.DisableParts();
    }
}
