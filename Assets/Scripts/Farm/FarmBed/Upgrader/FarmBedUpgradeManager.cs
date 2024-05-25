using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmBedUpgradeManager : MonoBehaviour, IUpgradeable
{
    [SerializeField] private FarmBedUpgrade[] _allUpgrades;
    private readonly List<FarmBedUpgrade> _availableUpgrades = new();

    public int UpgradesCount => _allUpgrades.Length;

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_allUpgrades.Contains(upgrade))
            _availableUpgrades.Add(upgrade as FarmBedUpgrade);
    }

    public bool ContainsUpgrade(FarmBedUpgrade upgrade)
    {
        return _availableUpgrades.Contains(upgrade);
    }

    public FarmBedUpgrade GetUpgradeByIndex(int index)
    {
        return _allUpgrades[index];
    }
}
