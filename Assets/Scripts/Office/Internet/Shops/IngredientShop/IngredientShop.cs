using UnityEngine;

public class IngredientShop : BaseInstantShop<Ingredient, OfficeData>, IUpgradeable {
    [SerializeField] private BedTypeManager _bedTypesManager;
    [SerializeField] private KitchenStorage _ingredientStorage;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _spiceAutoBuy;

    private void Awake() {
        _bedTypesManager.ItemAdded += delegate { UpdatePanels(); };
    }

    public void CheckUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _spiceAutoBuy) {
            Ingredient spice = ConstIngredients.Instance.Spice;
            int index = _data.IndexOfItemPanel(spice);
            RemoveItemPanel(spice, index);
        }
    }

    protected override void RemoveItemPanel(Ingredient item, int index) {
        if (item.Type == IngredientType.Buyable)
            _ingredientStorage.PutIngredientWithRemain(new IngredientCount(item, 1));
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
}
