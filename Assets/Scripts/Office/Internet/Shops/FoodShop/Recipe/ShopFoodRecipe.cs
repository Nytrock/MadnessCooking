using System.Linq;

public class ShopFoodRecipe : FoodRecipe<ShopFoodRecipePart> {
    protected override void SetupIngredients(Food food, ref bool canCook) {
        int index = 0;
        foreach (var ingredientCount in food.Ingredients.Select((value, index) => new { value, index })) {
            bool haveCount = _kitchenStorage.HaveCount(ingredientCount.value);
            _canCook &= haveCount;
            _recipeParts[index].Setup(ingredientCount.value, haveCount);
            index++;
        }
    }

    protected override void SetupTechnic(Technic technic, int index, ref bool canCook) {
        bool haveTechnic = _technicManager.HaveTechnic(technic);
        _canCook &= haveTechnic;
        _recipeParts[index].Setup(technic, haveTechnic);
    }

    public override void DisableParts() {
        foreach (var part in _recipeParts)
            part.gameObject.SetActive(false);
    }
}
