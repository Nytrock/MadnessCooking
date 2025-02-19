using System;
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
        Func<Ingredient, int> sortMethod = (ingredient) => ingredient.Type == IngredientType.Buyable ? 0 : ingredient.Price;
        _data.OrderItems(sortMethod);
    }

    protected override void RemoveItem(Ingredient item, int index) {
        if (item.Type == IngredientType.Buyable)
            _ingredientStorage.PutIngredientWithRemain(item, 1);
        else
            base.RemoveItem(item, index);
    }

    protected override bool IsBuyable(Ingredient ingredient) {
        if (ingredient.Type == IngredientType.Buyable)
            return true;

        return _bedTypesManager.HaveBedForIngredient(ingredient);
    }

    protected override GrayscaleImageData GenerateSideInfo(Ingredient ingredient) {
        if (ingredient.Type == IngredientType.Buyable)
            return null;

        BedType bedType = _bedTypesManager.GetBedByIngredientType(ingredient.Type);
        bool isBedAvailable = _bedTypesManager.IsItemAvailable(bedType);
        return new GrayscaleImageData(bedType.Icon, !isBedAvailable);
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
        }
    }
}
