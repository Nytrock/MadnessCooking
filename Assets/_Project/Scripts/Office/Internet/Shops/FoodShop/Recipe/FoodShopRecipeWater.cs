using UnityEngine;

public class FoodShopRecipeWater : FoodRecipeAdditionalPart {
    [SerializeField] private string _waterName = "Water";
    private Ingredient _water;

    protected override void Awake() {
        base.Awake();
        _water = Ingredient.CreateIngredient(_waterName);
    }

    public void Setup(bool isNeedWater, bool isWaterAvailable) {
        _showingItem = _water;
        _icon.SetGrayscaleVisibility(isWaterAvailable);
        ChangeState(isNeedWater);
    }
}
