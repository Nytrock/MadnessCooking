using UnityEngine;

public class FoodShopRecipeWater : FoodRecipeAdditionalPart {
    [SerializeField] private Ingredient _water;

    public void Setup(bool isNeedWater, bool isWaterAvailable) {
        _showingItem = _water;
        ChangeState(isNeedWater);
        _icon.SetGrayscaleVisibility(isWaterAvailable);
    }
}
