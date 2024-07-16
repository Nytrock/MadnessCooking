using UnityEngine;

public class FoodShopRecipe : FoodRecipe<FoodShopRecipePart> {

    [SerializeField] protected IngredientsManager _ingredientManager;

    protected override void SetupIngredients(ref bool canCook) {
        int index = 0;
        foreach (var ingredientCount in _food.Ingredients) {
            bool haveCount = _ingredientManager.HaveIngredient(ingredientCount.Item);
            _canCook &= haveCount;
            _recipeParts[index].Setup(ingredientCount, haveCount);
            index++;
        }
    }

    protected override void SetupTechnic(ref bool canCook) {
        bool haveTechnic = _technicManager.HaveTechnic(_food.TypeTechnic);
        _canCook &= haveTechnic;
        _techicIcon.Setup(_food.TypeTechnic.Icon, haveTechnic);
    }

    public override void DisableParts() {
        foreach (var part in _recipeParts)
            part.gameObject.SetActive(false);
    }
}
