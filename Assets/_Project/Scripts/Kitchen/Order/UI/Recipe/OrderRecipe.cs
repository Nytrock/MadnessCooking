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
                _recipeParts[index].SetupAutoSpice(count);
            } else {
                bool haveCount = _kitchenStorage.HaveCount(count);
                _recipeParts[index].Setup(count, haveCount);
            }
            index++;
        }
    }

    protected override void SetupTechnic() {
        bool haveTechnic = _technicManager.HaveTechnic(_food.TypeTechnic);
        _techicIcon.SetTechnic(_food.TypeTechnic, haveTechnic);
    }

    public void UpdateRecipeIngredients() {
        foreach (var part in _recipeParts) {
            if (part.IngredientCount is null)
                continue;

            bool haveCount = _kitchenStorage.HaveCount(part.IngredientCount);
            part.UpdateAvailable(haveCount);
        }
    }

    public void UpdateAutoSpices(int count) {
        if (!_upgradeData.IsAutoSpice)
            return;

        foreach (var part in _recipeParts) {
            if (part.IngredientCount is null)
                continue;

            if (part.IngredientCount.Item == ConstIngredients.Instance.Spice)
                part.UpdateAutoSpiceStatus(count);
        }
    }

    public void UpdateRecipeTechnic() => SetupTechnic();
}
