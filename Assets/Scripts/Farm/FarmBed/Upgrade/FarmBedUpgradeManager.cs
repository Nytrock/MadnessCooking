using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmBedUpgradeManager : MonoBehaviour, IUpgradeable
{
    [SerializeField] private FarmBedUpgrade[] _allUpgrades;
    private List<FarmBedUpgrade> _haveUpgrades = new();

    public int UpgradesCount => _allUpgrades.Length;

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_allUpgrades.Contains(upgrade))
            _haveUpgrades.Add(upgrade as FarmBedUpgrade);
    }

    public bool ContainsUpgrade(FarmBedUpgrade upgrade)
    {
        return _haveUpgrades.Contains(upgrade);
    }

    public FarmBedUpgrade GetUpgradeByIndex(int index)
    {
        return _allUpgrades[index];
    }
}
