using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmShop : BaseChooseShop
{
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private List<LimitedConsumableUpgradeHolder> _upgradesHolders;
    [SerializeField] private List<BaseUpgrade> _upgradesToBuy;
    private List<BaseUpgrade> _haveUpgrades = new();
    private bool _isBuyedItemReplaced;

    public bool IsBuyedItemReplaced => _isBuyedItemReplaced;

    public override void BuyItem(BuyableObject item)
    {
        base.BuyItem(item);

        var upgrade = item as BaseUpgrade;
        if (upgrade == null)
            return;

        MoneyManager.instance.ChangeMoney(-upgrade.Cost);
        _upgradeManager.NewUpgrade(upgrade);

        var index = Array.IndexOf(_itemsToBuy, upgrade);
        if (upgrade as LimitedConsumableUpgrade) {
            var consumableUpgrade = upgrade as LimitedConsumableUpgrade;
            foreach (LimitedConsumableUpgradeHolder upgradeHolder in _upgradesHolders) {
                if (upgradeHolder.ConsumableUpgrade == consumableUpgrade) {
                    upgradeHolder.AddCount();
                    _isBuyedItemReplaced = !upgradeHolder.IsMax;
                    if (upgradeHolder.IsMax) {
                        _haveUpgrades.Add(consumableUpgrade);
                        _upgradesHolders.Remove(upgradeHolder);
                        CheckNextUpgrades(consumableUpgrade, index);
                    }
                    break;
                }
            }
        } else if (upgrade as GraphUpgrade) {
            _isBuyedItemReplaced = false;
            _haveUpgrades.Add(upgrade);
            CheckNextUpgrades(upgrade as GraphUpgrade, index);
        } else {
            _upgradesToBuy.RemoveAt(index);
            _catalog.RemovePanel(index);
            _haveUpgrades.Add(upgrade);
        }
        SetObjectsArray();
    }

    private void CheckNextUpgrades(GraphUpgrade graphUpgrade, int index)
    {
        foreach (GraphUpgrade nextUpgrade in graphUpgrade.NextUpgrades) {
            if (_haveUpgrades.Contains(nextUpgrade)) {
                CheckNextUpgrades(nextUpgrade, index);
                continue;
            }
            bool canAdd = true;
            foreach (GraphUpgrade needUpgrade in nextUpgrade.NeedUpgrades) {
                canAdd &= _haveUpgrades.Contains(needUpgrade);
            }
            if (canAdd) {
                if (!_isBuyedItemReplaced) {
                    if (graphUpgrade as LimitedConsumableUpgrade)
                        _upgradesToBuy.Insert(index, nextUpgrade);
                    else
                        _upgradesToBuy[index - _upgradesHolders.Count] = nextUpgrade;
                    _catalog.UpdatePanel(index, nextUpgrade);
                    _isBuyedItemReplaced = true;
                } else {
                    _upgradesToBuy.Add(nextUpgrade);
                    _catalog.GeneratePanel(nextUpgrade);
                }
            }
        }
        if (!_isBuyedItemReplaced) {
            _upgradesToBuy.Remove(graphUpgrade);
            _catalog.RemovePanel(index);
        }
    }

    protected override void SetObjectsArray()
    {
        var upgradesConsumable = _upgradesHolders.Select(x => x.ConsumableUpgrade).ToList();
        _itemsToBuy = new BuyableObject[_upgradesToBuy.Count + upgradesConsumable.Count];
        int i;
        for (i = 0; i < upgradesConsumable.Count; i++) {
            _itemsToBuy[i] = upgradesConsumable[i];
        }
        for (int j = i; j < i + _upgradesToBuy.Count; j++) {
            _itemsToBuy[j] = _upgradesToBuy[j - i];
        }
    }
}
