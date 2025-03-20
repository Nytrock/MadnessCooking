using UnityEngine;

public class FoodShop : BaseChooseShop<Food, OfficeData> {
    [SerializeField] private IngredientManager _ingredientManager;
    [SerializeField] private TechnicManager _technicManager;

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

        if (!_technicManager.IsItemAvailable(food.TypeTechnic))
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

    public void CheckAddedUpgrade(BaseUpgrade upgrade) { }
}
