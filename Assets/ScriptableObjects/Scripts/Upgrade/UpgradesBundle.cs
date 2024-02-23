
using UnityEngine;

[CreateAssetMenu(menuName = nameof(BuyableObject) + "/" + nameof(UpgradesBundle))]
public class UpgradesBundle : BuyableObject
{
    [SerializeField] private Upgrade[] _upgradesList;

    public Upgrade[] UpgradesList => _upgradesList;
}
