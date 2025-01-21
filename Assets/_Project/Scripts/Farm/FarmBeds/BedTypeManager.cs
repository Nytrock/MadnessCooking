using System;
using System.Linq;
using UnityEngine;

public class BedTypeManager : SaveableItemManager<BedType, FarmData> {
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private IngredientsManager _ingredientsManager;

    [Header("Upgrades")]
    [SerializeField] private BedTypeUpgrade[] _bedsUpgrades;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckBedTypeAdded;
    }

    public override void AddItem(BedType item) {
        base.AddItem(item);
        if (item.AcceptableType == IngredientType.Ghost)
            _ingredientsManager.AddItem(ConstIngredients.Instance.Ectoplasm);
    }

    public BedType GetBedByIngredientType(IngredientType type) {
        foreach (var bed in _allItems)
            if (bed.AcceptableType == type)
                return bed;
        return null;
    }

    public bool HaveBedForIngredient(Ingredient ingredient) {
        foreach (var bed in _data.AvailableItems)
            if (bed.AcceptableType == ingredient.Type)
                return true;
        return false;
    }

    public void CheckBedTypeAdded(BaseUpgrade upgrade) {
        if (_bedsUpgrades.Contains(upgrade)) {
            var bedTypeUpgrade = upgrade as BedTypeUpgrade;
            AddItem(bedTypeUpgrade.BedType);
        }
    }

    public override void Bind(FarmData data) {
        data.BedTypeManager ??= new();
        _data = data.BedTypeManager;
    }
}
