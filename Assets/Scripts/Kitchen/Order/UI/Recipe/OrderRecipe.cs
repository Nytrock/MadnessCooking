public class OrderRecipe : FoodRecipe<OrderRecipePart> {
    private KitchenUpgradeData _upgradeData;

    public void Setup(KitchenStorage kitchenStorage, TechnicManager technicManager) {
        _kitchenStorage = kitchenStorage;
        _technicManager = technicManager;
    }

    public void SetupRecipe(Food food, KitchenUpgradeData data) {
        _upgradeData = data;
        SetupRecipe(food);
    }

    protected override void SetupIngredients(Food food, ref bool canCook) {
        int index = 0;
        foreach (var count in food.Ingredients) {
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

    protected override void SetupTechnic(Technic technic, int index, ref bool canCook) {
        bool haveTechnic = _technicManager.HaveTechnic(technic);
        _canCook &= haveTechnic;
        _techicIcon.Setup(technic.Icon, haveTechnic);
    }

    public override void DisableParts() {
        foreach (var part in _recipeParts)
            part.gameObject.SetActive(false);
    }
}
