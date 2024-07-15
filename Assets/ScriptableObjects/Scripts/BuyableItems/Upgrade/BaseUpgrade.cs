using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(BaseUpgrade))]
public class BaseUpgrade : BuyableItem, IGraphable<BaseUpgrade> {
    public new const string AssetMenuName = nameof(BuyableItem) + "/Upgrades/";

    [SerializeField] private BaseUpgrade[] _needUpgrades;
    [SerializeField] private BaseUpgrade[] _nextUpgrades;

    public IEnumerable<BaseUpgrade> NeedItems => _needUpgrades;
    public IEnumerable<BaseUpgrade> NextItems => _nextUpgrades;
}
