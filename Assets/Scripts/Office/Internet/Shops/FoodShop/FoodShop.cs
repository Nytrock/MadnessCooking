using UnityEngine;

public class FoodShop : BaseChooseShop<Food, OfficeData> {
    [SerializeField] private IngredientsManager _ingredientManager;
    [SerializeField] private TechnicManager _technicManager;

    protected override void Awake() {
        base.Awake();
        _ingredientManager.ItemAdded += delegate { UpdatePanels(); };
        _technicManager.ItemAdded += delegate { UpdatePanels(); };
    }

    protected override bool IsBuyable(Food food) {
        foreach (var ingredientCount in food.Ingredients)
            if (!_ingredientManager.HaveIngredient(ingredientCount.Ingredient))
                return false;

        if (!_technicManager.HaveTechnic(food.TypeTechnic))
            return false;

        return true;
    }

    public override void Bind(OfficeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.FoodShop = new(_defaultItemsToBuy);
        _data = data.FoodShop;
        base.Bind(data, isFileEmpty);
    }
}
