using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmShop : BaseChooseShop, IBindable<FarmData>
{
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private List<LimitedConsumableUpgradeHolder> _upgradesHolders;
    [SerializeField] private List<BaseUpgrade> _upgradesToBuy;
    private FarmData _data;

    public bool IsNoNextUpgrade { get; private set; }

    public override Type Type => typeof(BaseUpgrade);

    public override void BuyItem(BuyableObject item)
    {
        base.BuyItem(item);

        var upgrade = item as BaseUpgrade;
        if (upgrade == null)
            throw new NullReferenceException($"Buying item is not {Type}");

        MoneyManager.Instance.ChangeMoney(-upgrade.Price);
        _upgradeManager.NewUpgrade(upgrade);
        IsNoNextUpgrade = true;

        int index = Array.IndexOf(_itemsToBuy, upgrade);
        if (upgrade as LimitedConsumableUpgrade) {
            var consumableUpgrade = upgrade as LimitedConsumableUpgrade;
            foreach (var upgradeHolder in _upgradesHolders) {
                if (upgradeHolder.ConsumableUpgrade == consumableUpgrade) {
                    upgradeHolder.AddCount();
                    IsNoNextUpgrade = upgradeHolder.IsMax;
                    if (upgradeHolder.IsMax) {
                        _data.AvailableUpgrades.Add(consumableUpgrade);
                        _upgradesHolders.Remove(upgradeHolder);
                        CheckNextUpgrades(consumableUpgrade, index);
                    }
                    break;
                }
            }
        } else if (upgrade as GraphUpgrade) {
            _data.AvailableUpgrades.Add(upgrade);
            CheckNextUpgrades(upgrade as GraphUpgrade, index);
        } else {
            _upgradesToBuy.Remove(upgrade);
            _catalog.RemovePanel(index);
            _data.AvailableUpgrades.Add(upgrade);
        }

        SetObjectsArray();
    }

    private void CheckNextUpgrades(GraphUpgrade graphUpgrade, int index)
    {
        foreach (var nextUpgrade in graphUpgrade.NextUpgrades) {
            if (_upgradesToBuy.Contains(nextUpgrade))
                continue;

            if (_data.AvailableUpgrades.Contains(nextUpgrade)) {
                CheckNextUpgrades(nextUpgrade, index);
                continue;
            }

            bool canAdd = true;
            foreach (var needUpgrade in nextUpgrade.NeedUpgrades)
                canAdd &= _data.AvailableUpgrades.Contains(needUpgrade);

            if (canAdd) {
                if (IsNoNextUpgrade) {
                    if (graphUpgrade as LimitedConsumableUpgrade)
                        _upgradesToBuy.Insert(index, nextUpgrade);
                    else
                        _upgradesToBuy[index] = nextUpgrade;
                    _catalog.UpdatePanel(index, nextUpgrade);
                    IsNoNextUpgrade = false;
                } else {
                    _upgradesToBuy.Add(nextUpgrade);
                    _catalog.GeneratePanel(nextUpgrade);
                }
            }
        }

        if (IsNoNextUpgrade) {
            _upgradesToBuy.Remove(graphUpgrade);
            _catalog.RemovePanel(index);
        }
    }

    protected override void SetObjectsArray()
    {
        _data.UpgradesHolders = _upgradesHolders.ToArray();
        _data.UpgradesToBuy = _upgradesToBuy.ToArray();
        _itemsToBuy = _data.UpgradesToBuy;
    }

    public void Bind(FarmData data, bool isFileEmpty)
    {
        _data = data;
        if (!isFileEmpty) {
            _upgradesHolders = _data.UpgradesHolders.ToList();
            _upgradesToBuy = _data.UpgradesToBuy.ToList();
        }
        LateStart();
    }
}
