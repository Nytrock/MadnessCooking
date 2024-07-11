using System;
using UnityEngine;

public class IngredientShop : BaseInstantShop<Ingredient, OfficeData>, IUpgradeable<KitchenUpgradeData> {
    [SerializeField] private BedTypeManager _bedTypesManager;
    [SerializeField] private KitchenStorage _ingredientStorage;
    private KitchenUpgradeData _upgradeData;

    private void Awake() {
        _bedTypesManager.ItemAdded += delegate { UpdatePanels(); };
    }

    protected override void SortItems() {
        Func<Ingredient, int> sortMethod = (ingredient) => ingredient.Type == IngredientType.Buyable ? 0 : ingredient.Price;
        _data.OrderItems(sortMethod);
    }

    protected override void RemoveItemPanel(Ingredient item, int index) {
        if (item.Type == IngredientType.Buyable)
            _ingredientStorage.PutIngredientWithRemain(item, 1);
        else
            base.RemoveItemPanel(item, index);
    }

    protected override bool IsBuyable(Ingredient ingredient) {
        if (ingredient.Type == IngredientType.Buyable)
            return true;

        return _bedTypesManager.HaveBedForIngredient(ingredient);
    }

    protected override GrayscaleImageData GenerateSideInfo(Ingredient ingredient) {
        if (ingredient.Type == IngredientType.Buyable)
            return null;

        BedType bedType = _bedTypesManager.GetBedWithIngredientType(ingredient.Type);
        bool isBedAvailable = _bedTypesManager.HaveBed(bedType);
        return new GrayscaleImageData(bedType.Icon, isBedAvailable);
    }

    public override void Bind(OfficeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.IngredientShop = new(_defaultItemsToBuy);
        _data = data.IngredientShop;
        base.Bind(data, isFileEmpty);
    }

    public void BindUpgrade(KitchenUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (_upgradeData.IsAutoSpice) {
            Ingredient spice = ConstIngredients.Instance.Spice;
            int index = _data.IndexOfItemPanel(spice);
            RemoveItemPanel(spice, index);
        }
    }
}
