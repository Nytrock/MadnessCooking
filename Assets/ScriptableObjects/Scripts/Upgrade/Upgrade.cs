using UnityEngine;

[CreateAssetMenu(menuName = nameof(BuyableObject) + "/" + nameof(Upgrade))]

public class Upgrade : BuyableObject
{
    [SerializeField] private UpgradeType _typeUpgrade;

    public UpgradeType TypeUpgrade => _typeUpgrade;
}
