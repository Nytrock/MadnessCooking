using UnityEngine;

public class IngredientShop : BaseInstantShop<Ingredient, OfficeData>, IUpgradeable<KitchenUpgradeData> {
    [SerializeField] private BedTypeManager _bedTypesManager;
    [SerializeField] private KitchenStorage _ingredientStorage;
    private KitchenUpgradeData _upgradeData;

    public override void GenerateShop() {
        base.GenerateShop();
        _bedTypesManager.ItemAdded += delegate { UpdatePanels(); };
    }

    protected override void SortItems() {
        static int sortMethod(Ingredient ingredient) => ingredient.Type == IngredientType.Buyable ? 0 : ingredient.Price;
        _data.OrderItems(sortMethod);
    }

    protected override void RemoveItem(Ingredient item, int index) {
        if (item.Type == IngredientType.Buyable) {
            _ingredientStorage.PutIngredientWithRemain(item, 1);
            _itemManager.AddItem(item);
        } else {
            base.RemoveItem(item, index);
        }
    }

    protected override BuyPanelSideInfoData GenerateSideInfo(Ingredient ingredient) {
        if (ingredient.Type == IngredientType.Buyable)
            return null;

        BedType bedType = _bedTypesManager.GetBedByIngredientType(ingredient.Type);
        bool isBedAvailable = _bedTypesManager.IsItemAvailable(bedType);
        return new BuyPanelSideInfoData(bedType.Icon, !isBedAvailable, bedType.RawName);
    }

    public override void Bind(OfficeData data) {
        data.IngredientShop ??= new(_defaultItemsToBuy);
        _data = data.IngredientShop;
        base.Bind(data);
    }

    public void BindUpgrade(KitchenUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        Ingredient spice = ConstIngredients.Instance.Spice;
        if (_upgradeData.IsAutoSpice && _data.IsItemBuyable(spice)) {
            int index = _data.IndexOfItem(spice);
            base.RemoveItem(spice, index);
            _ingredientStorage.RemoveAllSpices();
        }
    }
}
