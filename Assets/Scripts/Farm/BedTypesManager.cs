using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BedTypesManager : MonoBehaviour, IUpgradeable, IBindable<FarmData>
{
    [SerializeField] private List<BedType> _allBeds;
    [SerializeField] private List<BedType> _defaultBeds;
    private FarmData _data;

    [Header("Upgrades")]
    [SerializeField] private BedTypeUpgrade[] _bedsUpgrades;

    public int BedsCount => _allBeds.Count;

    public event Action<BedType> TypeAdded;

    public BedType GetBed(int index)
    {
        return _allBeds[index];
    }

    public BedType GetBedWithIngredientType(IngredientType type)
    {
        foreach (var bed in _allBeds)
            if (bed.AcceptableType == type)
                return bed;
        return null;
    }

    public bool HaveBed(BedType bedType)
    {
        foreach (var bed in _data.AvailableBedTypes)
            if (bed == bedType)
                return true;
        return false;
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_bedsUpgrades.Contains(upgrade)) {
            var bedTypeUpgrade = upgrade as BedTypeUpgrade;
            AddBedType(bedTypeUpgrade.BedType);
        }
    }

    private void AddBedType(BedType newBedType)
    {
        _data.AvailableBedTypes.Add(newBedType);
        TypeAdded?.Invoke(newBedType);
    }

    public void Bind(FarmData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty)
            _data.AvailableBedTypes = _defaultBeds;
    }
}
