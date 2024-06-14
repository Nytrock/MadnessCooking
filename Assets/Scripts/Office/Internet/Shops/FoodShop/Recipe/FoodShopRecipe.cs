using UnityEngine;

public class FoodShopRecipe : FoodRecipe<FoodShopRecipePart> {

    [SerializeField] protected IngredientsManager _ingredientManager;

    protected override void SetupIngredients(Food food, ref bool canCook) {
        int index = 0;
        foreach (var ingredientCount in food.Ingredients) {
            bool haveCount = _ingredientManager.HaveIngredient(ingredientCount.Ingredient);
            _canCook &= haveCount;
            _recipeParts[index].Setup(ingredientCount, haveCount);
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
