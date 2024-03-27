using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmBedUpgradeManager : MonoBehaviour, IUpgradeable
{
    [SerializeField] private GroundBedUpgrade[] _allUpgrades;
    private List<GroundBedUpgrade> _haveUpgrades = new();

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_allUpgrades.Contains(upgrade))
            _haveUpgrades.Add(upgrade as GroundBedUpgrade);
    }
}
