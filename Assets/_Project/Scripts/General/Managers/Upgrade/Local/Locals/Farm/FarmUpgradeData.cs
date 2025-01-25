using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class FarmUpgradeData : ISaveable {
    [SerializeField, JsonProperty] private bool _isGrowStatusShow;
    [SerializeField, JsonProperty] private bool _isWheatDistributing;
    [SerializeField, JsonProperty] private bool _isPuncherProgressShow;
    [SerializeField, JsonProperty] private List<FarmBedUpgrade> _availableFarmBedUpgrades;

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
        _availableFarmBedUpgrades = _availableFarmBedUpgrades.OrderBy(upgrade => upgrade.Price).ToList();
    }

    public void ChangePuncherProgressShow() {
        _isPuncherProgressShow = true;
    }

    public bool ContainsFarmBedUpgrade(FarmBedUpgrade upgrade) {
        return _availableFarmBedUpgrades.Contains(upgrade);
    }
}
