using UnityEngine;

public class FoodShop : BaseChooseShop<Food, OfficeData>, IUpgradeable<KitchenUpgradeData> {
    [SerializeField] private IngredientManager _ingredientManager;
    [SerializeField] private TechnicManager _technicManager;
    private KitchenUpgradeData _upgradeData;

    public override void GenerateShop() {
        base.GenerateShop();
        _ingredientManager.ItemAdded += delegate { UpdatePanels(); };
        _technicManager.ItemAdded += delegate { UpdatePanels(); };
    }

    public override void ChangeShopState(bool newState) {
        base.ChangeShopState(newState);
        if (!newState && _itemToBuy != null)
            ChooseItem(_itemToBuy);
    }

    protected override bool IsBuyable(Food food) {
        if (food == null)
            return false;

        if (!_technicManager.HaveTechnic(food.TypeTechnic))
            return false;

        foreach (var ingredientCount in food.Ingredients)
            if (!_ingredientManager.IsItemAvailable(ingredientCount.Ingredient))
                return false;

        return true;
    }

    public override void Bind(OfficeData data) {
        data.FoodShop ??= new(_defaultItemsToBuy);
        _data = data.FoodShop;
        base.Bind(data);
    }

    public void BindUpgrade(KitchenUpgradeData upgradeData) {
        _upgradeData = upgradeData;
        (_itemView as FoodShopItemView).SetUpgradeDataToRecipe(_upgradeData);
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) { }
}
