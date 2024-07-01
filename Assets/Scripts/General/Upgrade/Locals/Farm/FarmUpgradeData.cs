using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FarmUpgradeData : LocalUpgradeData {
    [SerializeField] private bool _isAutoWheat;
    [SerializeField] private bool _isGrowStatusShow;
    [SerializeField] private bool _isWheatDistributing;
    [SerializeField] private List<FarmBedUpgrade> _availableFarmBedUpgrades;

    public bool IsAutoWheat => _isAutoWheat;
    public bool IsGrowStatusShow => _isGrowStatusShow;
    public bool IsWheatDistributing => _isWheatDistributing;

    public void ChangeAutoWheat() {
        _isAutoWheat = true;
    }

    public void ChangeGrowStatusShow() {
        _isGrowStatusShow = true;
    }

    public void ChangeWheatDistributing() {
        _isWheatDistributing = true;
    }

    public void AddFarmBedUpgrade(FarmBedUpgrade farmBedUpgrade) {
        _availableFarmBedUpgrades.Add(farmBedUpgrade);
    }

    public bool ContainsFarmBedUpgrade(FarmBedUpgrade upgrade) {
        return _availableFarmBedUpgrades.Contains(upgrade);
    }
}
