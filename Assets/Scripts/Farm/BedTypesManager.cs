using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BedTypesManager : MonoBehaviour, IUpgradeable
{
    [SerializeField] private List<BedType> _allBeds;
    [SerializeField] private List<BedType> _haveBeds;

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
        foreach (var bed in _haveBeds)
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
        _haveBeds.Add(newBedType);
        TypeAdded?.Invoke(newBedType);
    }
}
