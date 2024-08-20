using UnityEngine;

public class FoodShopRecipe : FoodRecipe<FoodShopRecipePart> {
    [SerializeField] private FoodShopRecipeWater _waterIcon;
    [SerializeField] protected IngredientsManager _ingredientManager;
    private KitchenUpgradeData _upgradeData;

    public override void SetupRecipe(Food food) {
        base.SetupRecipe(food);
        SetupWater(food);
    }

    public override void SetHoverText(HoverText hoverText) {
        base.SetHoverText(hoverText);
        _waterIcon.SetHoverText(hoverText);
    }

    protected override void SetupIngredients() {
        int index = 0;
        foreach (var ingredientCount in _food.Ingredients) {
            bool haveCount = _ingredientManager.HaveIngredient(ingredientCount.Item);
            _canCook &= haveCount;
            _recipeParts[index].Setup(ingredientCount, haveCount);
            index++;
        }
    }

    protected override void SetupTechnic() {
        bool haveTechnic = _technicManager.HaveTechnic(_food.TypeTechnic);
        _canCook &= haveTechnic;
        _techicIcon.SetTechnic(_food.TypeTechnic, haveTechnic);
    }

    private void SetupWater(Food food) {
        _waterIcon.Setup(food.IsNeedWater, _upgradeData.IsWaterAvailable);
    }

    public override void DisableParts() {
        foreach (var part in _recipeParts)
            part.gameObject.SetActive(false);
        _waterIcon.ChangeState(false);
        _techicIcon.ChangeState(false);
    }

    public void SetUpgradeData(KitchenUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }
}
