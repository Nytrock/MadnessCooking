public class OrderRecipe : FoodRecipe<OrderRecipePart> {
    private KitchenData _data;

    public void Setup(KitchenStorage kitchenStorage, TechnicManager technicManager) {
        _kitchenStorage = kitchenStorage;
        _technicManager = technicManager;
    }

    public void SetupRecipe(Food food, KitchenData data) {
        _data = data;
        SetupRecipe(food);
    }

    protected override void SetupIngredients(IngredientCountList ingredients, ref bool canCook) {
        for (int i = 0; i < ingredients.Size; i++) {
            IngredientCount ingredientCount = ingredients.Get(i);
            if (ingredientCount.Ingredient == ConstIngredients.Instance.Spice && _data.IsAutoSpice) {
                _canCook &= MoneyManager.Instance.MoneyCount >= ingredientCount.Count * ingredientCount.Ingredient.Price;
                _recipeParts[i].SetupAutoSpice(ingredientCount);
            } else {
                bool haveCount = _kitchenStorage.HaveCount(ingredientCount);
                _canCook &= haveCount;
                _recipeParts[i].Setup(ingredientCount, haveCount);
            }
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
