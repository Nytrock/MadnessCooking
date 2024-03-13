public class ShopFoodRecipe : FoodRecipe<ShopFoodRecipePart>
{
    protected override void SetupIngredients(IngredientCountList ingredients, ref bool canCook)
    {
        for (int i = 0; i < ingredients.Size; i++) {
            bool haveCount = _kitchenStorage.HaveCount(ingredients.Get(i));
            _canCook &= haveCount;
            _recipeParts[i].Setup(ingredients.Get(i), haveCount);
        }
    }

    protected override void SetupTechnic(Technic technic, int index, ref bool canCook)
    {
        bool haveTechnic = _technicManager.HaveTechnic(technic);
        _canCook &= haveTechnic;
        _recipeParts[index].Setup(technic, haveTechnic);
    }

    public override void DisableParts()
    {
        foreach (var part in _recipeParts)
            part.gameObject.SetActive(false);
    }
}
