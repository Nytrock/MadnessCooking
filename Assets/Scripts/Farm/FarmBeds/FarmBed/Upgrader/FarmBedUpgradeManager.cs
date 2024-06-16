using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmBedUpgradeManager : MonoBehaviour, IUpgradeable<FarmUpgradeData> {
    [SerializeField] private FarmBedUpgrade[] _allUpgrades;
    private FarmUpgradeData _upgradeData;

    public IEnumerable<FarmBedUpgrade> GetAvailableUpgrades() {
        foreach (var upgrade in _allUpgrades)
            yield return upgrade;
    }

    public bool ContainsUpgrade(FarmBedUpgrade upgrade) {
        return _upgradeData.ContainsFarmBedUpgrade(upgrade);
    }

    public void BindUpgrade(FarmUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (_allUpgrades.Contains(upgrade))
            _upgradeData.AddFarmBedUpgrade(upgrade as FarmBedUpgrade);
    }
}
