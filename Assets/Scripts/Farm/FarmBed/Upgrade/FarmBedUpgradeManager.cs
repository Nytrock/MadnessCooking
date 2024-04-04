using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmBedUpgradeManager : MonoBehaviour, IUpgradeable
{
    [SerializeField] private FarmBedUpgrade[] _allUpgrades;
    private List<FarmBedUpgrade> _haveUpgrades = new();

    public int HaveUpgradesCount => _haveUpgrades.Count;

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_allUpgrades.Contains(upgrade))
            _haveUpgrades.Add(upgrade as FarmBedUpgrade);
    }

    public FarmBedUpgrade GetUpgradeByIndex(int index)
    {
        return _haveUpgrades[index];
    }

    public int GetIndexOfUpgrade(FarmBedUpgrade upgrade)
    {
        return _haveUpgrades.IndexOf(upgrade);
    }
}
