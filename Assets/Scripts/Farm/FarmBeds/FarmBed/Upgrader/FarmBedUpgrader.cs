using System.Collections.Generic;
using UnityEngine;

public class FarmBedUpgrader : MonoBehaviour {
    [SerializeField] private FarmBedUpgrade _eternalWater;
    [SerializeField] private FarmBedUpgrade _eternalFertilize;
    [SerializeField] private CoefficientFarmBedUpgrade _technicSpeedUp;
    [SerializeField] private FarmBedUpgrade _instantPests;
    [SerializeField] private FarmBedUpgrade _pestsRemove;

    private readonly List<FarmBedUpgrade> _availableUpgrades = new();
    private FarmBedData _bedData;

    public void AddUpgrade(FarmBedUpgrade upgrade) {
        if (upgrade == _eternalWater)
            _bedData.WaterBoost.BecomeEternal();
        else if (upgrade == _eternalFertilize)
            _bedData.FertilizeBoost.BecomeEternal();
        else if (upgrade == _technicSpeedUp)
            _bedData.SetIndependentBoost(_technicSpeedUp);
        else if (upgrade == _instantPests)
            _bedData.PestsGenerator.SetInstantUpgrade();
        else if (upgrade == _pestsRemove)
            _bedData.PestsGenerator.SetRemoveUpgrade();
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
    }
}
