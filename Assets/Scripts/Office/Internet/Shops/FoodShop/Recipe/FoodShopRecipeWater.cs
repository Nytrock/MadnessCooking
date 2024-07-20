using UnityEngine;

public class FoodShopRecipeWater : FoodRecipeAdditionalPart {
    [SerializeField] private string _waterName;

    public void Setup(bool isNeedWater, bool isWaterAvailable) {
        _showingMessage = _waterName;
        ChangeState(isNeedWater);
        _icon.SetGrayscaleVisibility(isWaterAvailable);
    }
}
