using UnityEngine;

public class FoodShop : BaseChooseShop<Food, OfficeData>, IUpgradeable<KitchenUpgradeData> {
    [SerializeField] private IngredientsManager _ingredientManager;
    [SerializeField] private TechnicManager _technicManager;
    private KitchenUpgradeData _upgradeData;

    public override void GenerateShop() {
        base.GenerateShop();
        _ingredientManager.ItemAdded += delegate { UpdatePanels(); };
        _technicManager.ItemAdded += delegate { UpdatePanels(); };
    }

    protected override bool IsBuyable(Food food) {
        if (food == null)
            return false;

        if (food.IsNeedWater && !_upgradeData.IsWaterAvailable)
            return false;

        if (!_technicManager.HaveTechnic(food.TypeTechnic))
            return false;

        foreach (var ingredientCount in food.Ingredients)
            if (!_ingredientManager.IsItemAvailable(ingredientCount.Item))
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
