using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : BaseInstantShop
{
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private List<BaseUpgrade> _upgradesToBuy;

    public override void BuyItem(BuyableObject item)
    {
        var upgrade = item as BaseUpgrade;
        if (upgrade == null)
            return;

        MoneyManager.instance.ChangeMoney(-upgrade.Cost);
        _upgradeManager.NewUpgrade(upgrade);

        var index = _upgradesToBuy.IndexOf(upgrade);
        var progressUpgrade = upgrade as ProgressUpgrade;
        if (progressUpgrade != null && progressUpgrade.nextUpgrade != null) {
            _upgradesToBuy[index] = progressUpgrade.nextUpgrade;
            _catalog.UpdatePanel(index, progressUpgrade.nextUpgrade);
        } else {
            _upgradesToBuy.RemoveAt(index);
            _catalog.RemovePanel(index);
            SetObjectsArray();
        }
    }

    protected override void SetObjectsArray()
    {
        _itemsToBuy = _upgradesToBuy.ToArray();
    }
}
