using System;
using System.Linq;
using UnityEngine;

public class BedTypeManager : SaveableItemManager<BedType, FarmData> {
    [SerializeField] private BedType[] _allBeds;
    [SerializeField] private UpgradeManager _upgradeManager;

    [Header("Upgrades")]
    [SerializeField] private BedTypeUpgrade[] _bedsUpgrades;

    public int BedsCount => _allBeds.Length;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckBedTypeAdded;
    }

    public BedType GetBed(int index) {
        return _allBeds[index];
    }

    public BedType GetBedWithIngredientType(IngredientType type) {
        foreach (var bed in _allBeds)
            if (bed.AcceptableType == type)
                return bed;
        return null;
    }

    public bool HaveBed(BedType bedType) {
        foreach (var bed in _data.AvailableItems)
            if (bed == bedType)
                return true;
        return false;
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
        base.Bind(data);
    }
}
