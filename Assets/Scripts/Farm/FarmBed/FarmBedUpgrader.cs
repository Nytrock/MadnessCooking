using System.Collections.Generic;
using UnityEngine;

public class FarmBedUpgrader : MonoBehaviour
{
    [SerializeField] private FarmBedUpgrade _eternalWater;
    [SerializeField] private FarmBedUpgrade _eternalFertilize;
    [SerializeField] private CoefficientFarmBedUpgrade _technicSpeedUp;
    [SerializeField] private FarmBedUpgrade _instantPests;
    [SerializeField] private FarmBedUpgrade _pestsRemove;

    private readonly List<FarmBedUpgrade> _availableUpgrades = new();
    private SerializableFarmBed _bedData;

    public void AddUpgrade(FarmBedUpgrade upgrade)
    {
        if (upgrade == _eternalWater)
            _bedData.WaterBoost.IsEternal = true;
        else if (upgrade == _eternalFertilize)
            _bedData.FertilizeBoost.IsEternal = true;
        else if (upgrade == _technicSpeedUp)
            _bedData.IndependentBoost = _technicSpeedUp.Coefficient;
        else if (upgrade == _instantPests)
            _bedData.PestsGenerator.IsPestsInstant = true;
        else if (upgrade == _pestsRemove)
            _bedData.PestsGenerator.IsPestsRemoved = true;
        _availableUpgrades.Add(upgrade);
    }

    public bool HaveUpgrade(FarmBedUpgrade upgrade)
    {
        return _availableUpgrades.Contains(upgrade);
    }

    public void DisableUpgrades()
    {
        foreach (var upgrade in _availableUpgrades)
            MoneyManager.Instance.ChangeMoney(upgrade.Cost);

        _bedData.WaterBoost.IsEternal = false;
        _bedData.FertilizeBoost.IsEternal = false;
        _bedData.PestsGenerator.IsPestsInstant = false;
        _bedData.PestsGenerator.IsPestsRemoved = false;
    }

    public void Bind(SerializableFarmBed bedData)
    {
        _bedData = bedData;
    }
}
