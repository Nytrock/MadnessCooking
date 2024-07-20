using UnityEngine;

public class FoodShop : BaseChooseShop<Food, OfficeData>, IUpgradeable<KitchenUpgradeData> {
    [SerializeField] private IngredientsManager _ingredientManager;
    [SerializeField] private TechnicManager _technicManager;
    private KitchenUpgradeData _upgradeData;

    protected override void Awake() {
        base.Awake();
        _ingredientManager.ItemAdded += delegate { UpdatePanels(); };
        _technicManager.ItemAdded += delegate { UpdatePanels(); };
    }

    protected override bool IsBuyable(Food food) {
        if (food.IsNeedWater && !_upgradeData.IsWaterAvailable)
            return false;

        if (!_technicManager.HaveTechnic(food.TypeTechnic))
            return false;

        foreach (var ingredientCount in food.Ingredients)
            if (!_ingredientManager.HaveIngredient(ingredientCount.Item))
                return false;

        return true;
    }

    public override void Bind(OfficeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.FoodShop = new(_defaultItemsToBuy);
        _data = data.FoodShop;
        base.Bind(data, isFileEmpty);
    }

    public void BindUpgrade(KitchenUpgradeData upgradeData) {
        _upgradeData = upgradeData;
        (_itemView as FoodShopItemView).SetUpgradeDataToRecipe(_upgradeData);
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) { }
}
