using UnityEngine;

public class FoodShopRecipe : FoodRecipe<FoodShopRecipePart> {
    [SerializeField] protected IngredientsManager _ingredientManager;
    private KitchenUpgradeData _upgradeData;

    protected override void SetupIngredients() {
        int index = 0;
        foreach (var ingredientCount in _food.Ingredients) {
            bool haveCount = _ingredientManager.IsItemAvailable(ingredientCount.Item);
            _recipeParts[index].Setup(ingredientCount, haveCount);
            index++;
        }
    }

    protected override void SetupTechnic() {
        bool haveTechnic = _technicManager.HaveTechnic(_food.TypeTechnic);
        _techicIcon.SetTechnic(_food.TypeTechnic, haveTechnic);
    }

    public override void DisableParts() {
        base.DisableParts();
        _techicIcon.ChangeState(false);
    }

    public void SetUpgradeData(KitchenUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }
}
