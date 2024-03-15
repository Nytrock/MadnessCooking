using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : BaseInstantShop
{
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private List<BaseUpgrade> _upgradesToBuy;
    private List<BaseUpgrade> _haveUpgrades = new();

    public override void BuyItem(BuyableObject item)
    {
        var upgrade = item as BaseUpgrade;
        if (upgrade == null)
            return;

        MoneyManager.instance.ChangeMoney(-upgrade.Cost);
        _upgradeManager.NewUpgrade(upgrade);
        _haveUpgrades.Add(upgrade);

        var index = _upgradesToBuy.IndexOf(upgrade);
        if (upgrade as GraphUpgrade) {
            bool isFirst = true;
            CheckNextUpgrades(upgrade as GraphUpgrade, index, ref isFirst);
        } else {
            _upgradesToBuy.RemoveAt(index);
            _catalog.RemovePanel(index);
        }
        SetObjectsArray();
    }

    private void CheckNextUpgrades(GraphUpgrade graphUpgrade, int index, ref bool isFirst)
    {
        foreach (GraphUpgrade nextUpgrade in graphUpgrade.NextUpgrades) {
            if (_haveUpgrades.Contains(nextUpgrade)) {
                CheckNextUpgrades(nextUpgrade, index, ref isFirst);
                continue;
            }
            bool canAdd = true;
            foreach (GraphUpgrade needUpgrade in nextUpgrade.NeedUpgrades) {
                canAdd &= _haveUpgrades.Contains(needUpgrade);
            }
            if (canAdd) {
                if (isFirst) {
                    _upgradesToBuy[index] = nextUpgrade;
                    _catalog.UpdatePanel(index, nextUpgrade);
                    isFirst = false;
                } else {
                    _upgradesToBuy.Add(nextUpgrade);
                    _catalog.GeneratePanel(nextUpgrade);
                }
            }
        }
        if (isFirst) {
            _upgradesToBuy.RemoveAt(index);
            _catalog.RemovePanel(index);
        }
    }

    protected override void SetObjectsArray()
    {
        _itemsToBuy = _upgradesToBuy.ToArray();
    }
}
