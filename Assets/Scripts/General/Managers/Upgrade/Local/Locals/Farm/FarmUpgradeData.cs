using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FarmUpgradeData : ISaveable {
    [SerializeField] private bool _isGrowStatusShow;
    [SerializeField] private bool _isWheatDistributing;
    [SerializeField] private bool _isPuncherProgressShow;
    [SerializeField] private List<FarmBedUpgrade> _availableFarmBedUpgrades;

    public bool IsGrowStatusShow => _isGrowStatusShow;
    public bool IsPuncherProgressShow => _isPuncherProgressShow;
    public bool IsWheatDistributing => _isWheatDistributing;

    public FarmUpgradeData() {
        _availableFarmBedUpgrades = new();
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

    public void ChangePuncherProgressShow() {
        _isPuncherProgressShow = true;
    }

    public bool ContainsFarmBedUpgrade(FarmBedUpgrade upgrade) {
        return _availableFarmBedUpgrades.Contains(upgrade);
    }
}
