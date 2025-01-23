public class FoodShopRecipeWater : FoodRecipeAdditionalPart {

    public void Setup(bool isNeedWater, bool isWaterAvailable) {
        _showingItem = ConstIngredients.Instance.Water;
        _isAvailable = isWaterAvailable;
        _grayscaleIcon.SetGrayscaleVisibility(!isWaterAvailable);
        ChangeState(isNeedWater);
    }
}
