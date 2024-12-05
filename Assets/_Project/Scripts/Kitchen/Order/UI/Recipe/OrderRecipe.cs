public class OrderRecipe : FoodRecipe<OrderRecipePart> {
    private KitchenUpgradeData _upgradeData;

    public void SetManagers(KitchenStorage kitchenStorage, TechnicManager technicManager) {
        _kitchenStorage = kitchenStorage;
        _technicManager = technicManager;
    }

    public void SetupRecipe(Food food, KitchenUpgradeData data) {
        _upgradeData = data;
        SetupRecipe(food);
    }

    protected override void SetupIngredients() {
        int index = 0;
        foreach (var count in _food.Ingredients) {
            if (count.Item == ConstIngredients.Instance.Spice && _upgradeData.IsAutoSpice) {
                _canCook &= MoneyManager.Instance.MoneyCount >= count.Count * count.Item.Price;
                _recipeParts[index].SetupAutoSpice(count);
            } else {
                bool haveCount = _kitchenStorage.HaveCount(count);
                _canCook &= haveCount;
                _recipeParts[index].Setup(count, haveCount);
            }
            index++;
        }
    }

    protected override void SetupTechnic() {
        bool haveTechnic = _technicManager.HaveTechnic(_food.TypeTechnic);
        _canCook &= haveTechnic;
        _techicIcon.SetTechnic(_food.TypeTechnic, haveTechnic);
    }

    public void UpdateRecipeIngredients(BuyableItemCount<Ingredient> count) {
        foreach (var part in _recipeParts) {
            if (part.IngredientCount.Item == count.Item) {
                bool haveCount = _kitchenStorage.HaveCount(part.IngredientCount);
                _canCook &= haveCount;
                part.UpdateAvailable(haveCount);
            }
        }
    }

    public void UpdateRecipeTechnic() => SetupTechnic();
}
