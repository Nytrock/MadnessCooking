using System.Collections.Generic;
using UnityEngine;

public class FarmBedUpgrader : MonoBehaviour
{
    [Header("Upgrades")]
    [SerializeField] private FarmBedUpgrade _alwaysWater;
    [SerializeField] private FarmBedUpgrade _alwaysFertilize;
    [SerializeField] private FarmBedUpgrade _technicSpeedUp;
    [SerializeField] private float _technicSpeedBooster;
    [SerializeField] private FarmBedUpgrade _instantPests;
    [SerializeField] private FarmBedUpgrade _pestsRemove;

    private List<FarmBedUpgrade> _haveUpgrades = new();
    private BedTypeHolder _bedHolder;
    private FarmBed _farmBed;

    public float UpgradesBooster { get; private set; }
    public bool IsWatered { get; private set; }
    public bool IsFertilized { get; private set; }
    public bool IsPestsInstant { get; private set; }
    public bool IsPestsRemoved { get; private set; }

    private void Awake()
    {
        _farmBed = GetComponent<FarmBed>();
    }

    public void AddUpgrade(FarmBedUpgrade upgrade)
    {
        if (upgrade == _alwaysWater) {
            IsWatered = true;
            _bedHolder.ChangeEternalWater();
            _farmBed.EternalWater();
        } else if (upgrade == _alwaysFertilize) {
            IsFertilized = true;
            _bedHolder.ChangeEternalFertilize();
            _farmBed.EternalFertilize();
        } else if (upgrade == _technicSpeedUp) {
            UpgradesBooster = _technicSpeedBooster;
            _farmBed.UpdateUpgradeBooster();
        } else if (upgrade == _instantPests) {
            IsPestsInstant = true;
        } else if (upgrade == _pestsRemove) {
            IsPestsRemoved = true;
            _farmBed.RemovePests();
        }
        _haveUpgrades.Add(upgrade);
    }

    public bool HaveUpgrade(FarmBedUpgrade upgrade)
    {
        return _haveUpgrades.Contains(upgrade);
    }

    public void UpdateBedHolder(BedTypeHolder holder)
    {
        _bedHolder = holder;
    }
    
    public void ReturnUpgrades()
    {
        foreach (var upgrade in _haveUpgrades)
            MoneyManager.instance.ChangeMoney(upgrade.Cost);

        IsWatered = false;
        IsFertilized = false;
        IsPestsInstant = false;

        _bedHolder.ChangeEternalWater();
        _bedHolder.ChangeEternalFertilize();
        _farmBed.PestsGenerator.SetRemoved(_farmBed);
    }
}
