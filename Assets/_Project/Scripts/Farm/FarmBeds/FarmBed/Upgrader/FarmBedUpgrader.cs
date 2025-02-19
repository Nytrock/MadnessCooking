using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmBedUpgrader : MonoBehaviour {
    [SerializeField] private FarmBedUpgrade _eternalWater;
    [SerializeField] private FarmBedUpgrade _eternalFertilize;
    [SerializeField] private CoefficientFarmBedUpgrade _technicSpeedUp;
    [SerializeField] private FarmBedUpgrade[] _instantPests;
    [SerializeField] private FarmBedUpgrade[] _pestsRemove;
    [SerializeField] private FarmBedUpgrade _autoCollect;

    [SerializeField] private List<FarmBedUpgrade> _availableUpgrades = new();
    private FarmBedData _bedData;

    public void AddUpgrade(FarmBedUpgrade upgrade) {
        if (upgrade == _eternalWater)
            _bedData.WaterBoost.BecomeEternal();
        else if (upgrade == _eternalFertilize)
            _bedData.FertilizeBoost.BecomeEternal();
        else if (upgrade == _technicSpeedUp)
            _bedData.SetIndependentBoost(_technicSpeedUp);
        else if (_instantPests.Contains(upgrade))
            _bedData.PestsGenerator.SetInstantUpgrade();
        else if (_pestsRemove.Contains(upgrade))
            _bedData.PestsGenerator.SetRemoveUpgrade();
        else if (upgrade == _autoCollect)
            _bedData.SetAutoCollect();
        _availableUpgrades.Add(upgrade);
    }

    public bool HaveUpgrade(FarmBedUpgrade upgrade) {
        return _availableUpgrades.Contains(upgrade);
    }

    public void DisableUpgrades() {
        foreach (var upgrade in _availableUpgrades)
            MoneyManager.Instance.ChangeMoney((int)(upgrade.Price * 0.5f));
        _bedData.DisableUpgrades();
    }

    public void Bind(FarmBedData bedData) {
        _bedData = bedData;

        if (_bedData.WaterBoost.IsEternal)
            _availableUpgrades.Add(_eternalWater);

        if (_bedData.FertilizeBoost.IsEternal)
            _availableUpgrades.Add(_eternalFertilize);

        if (_bedData.IndependentBoost == _technicSpeedUp.Coefficient)
            _availableUpgrades.Add(_technicSpeedUp);

        if (_bedData.IsAutoCollect)
            _availableUpgrades.Add(_autoCollect);

        if (_bedData.PestsGenerator.IsPestsInstant)
            foreach (var upgrade in _instantPests)
                if (upgrade.SuitableBedTypes.Contains(_bedData.BedType))
                    _availableUpgrades.Add(upgrade);

        if (_bedData.PestsGenerator.IsPestsRemoved)
            foreach (var upgrade in _pestsRemove)
                if (upgrade.SuitableBedTypes.Contains(_bedData.BedType))
                    _availableUpgrades.Add(upgrade);
    }
}
