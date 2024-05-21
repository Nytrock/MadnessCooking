using System.Collections.Generic;
using UnityEngine;

public class FarmBedUpgrader : MonoBehaviour
{
    [Header("Upgrades")]
    [SerializeField] private FarmBedUpgrade _eternalWater;
    [SerializeField] private FarmBedUpgrade _eternalFertilize;
    [SerializeField] private CoefficientFarmBedUpgrade _technicSpeedUp;
    [SerializeField] private FarmBedUpgrade _instantPests;
    [SerializeField] private FarmBedUpgrade _pestsRemove;

    private List<FarmBedUpgrade> _haveUpgrades = new();
    private FarmBed _farmBed;

    private void Awake()
    {
        _farmBed = GetComponent<FarmBed>();
    }

    public void AddUpgrade(FarmBedUpgrade upgrade)
    {
        if (upgrade == _eternalWater) {
            _farmBed.BedData.WaterBoost.IsEternal = true;
            _farmBed.ChangeEternalWater();
        } else if (upgrade == _eternalFertilize) {
            _farmBed.BedData.FertilizeBoost.IsEternal = true;
            _farmBed.ChangeEternalFertilize();
        } else if (upgrade == _technicSpeedUp) {
            _farmBed.BedData.IndependentBoost = _technicSpeedUp.Coefficient;
        } else if (upgrade == _instantPests) {
            _farmBed.BedData.PestsGenerator.IsPestsInstant = true;
        } else if (upgrade == _pestsRemove) {
            _farmBed.BedData.PestsGenerator.IsPestsRemoved = true;
            _farmBed.RemovePests();
        }
        _haveUpgrades.Add(upgrade);
    }

    public bool HaveUpgrade(FarmBedUpgrade upgrade)
    {
        return _haveUpgrades.Contains(upgrade);
    }

    public void ReturnUpgrades()
    {
        foreach (var upgrade in _haveUpgrades)
            MoneyManager.instance.ChangeMoney(upgrade.Cost);

        _farmBed.BedData.WaterBoost.IsEternal = false;
        _farmBed.BedData.FertilizeBoost.IsEternal = false;
        _farmBed.BedData.PestsGenerator.IsPestsInstant = false;
        _farmBed.BedData.PestsGenerator.IsPestsRemoved = false;

        _farmBed.ChangeEternalWater();
        _farmBed.ChangeEternalFertilize();
    }
}
