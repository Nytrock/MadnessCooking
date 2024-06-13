using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeShop : BaseInstantShop, IBindable<OfficeData> {
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private List<BaseUpgrade> _upgradesToBuy;
    private OfficeData _data;

    public override Type Type => typeof(BaseUpgrade);

    public override void BuyItem(BuyableObject item) {
        var upgrade = item as BaseUpgrade;
        if (upgrade == null)
            throw new NullReferenceException($"Buying item is not {Type}");

        MoneyManager.Instance.ChangeMoney(-upgrade.Price);
        _upgradeManager.NewUpgrade(upgrade);
        _data.AvailableUpgrades.Add(upgrade);

        int index = _upgradesToBuy.IndexOf(upgrade);
        if (upgrade as GraphUpgrade) {
            bool isFirst = true;
            CheckNextUpgrades(upgrade as GraphUpgrade, index, ref isFirst);
        } else {
            _upgradesToBuy.RemoveAt(index);
            _catalog.RemovePanel(index);
        }
        SetObjectsArray();
    }

    private void CheckNextUpgrades(GraphUpgrade graphUpgrade, int index, ref bool isFirst) {
        foreach (var nextUpgrade in graphUpgrade.NextItems) {
            if (_data.AvailableUpgrades.Contains(nextUpgrade) || _upgradesToBuy.Contains(nextUpgrade)) {
                CheckNextUpgrades(nextUpgrade, index, ref isFirst);
                continue;
            }
            bool canAdd = true;
            foreach (var needUpgrade in nextUpgrade.NeedItems)
                canAdd &= _data.AvailableUpgrades.Contains(needUpgrade);
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

    protected override void SetObjectsArray() {
        _data.ShopUpgrades = _upgradesToBuy.ToArray();
        _itemsToBuy = _data.ShopUpgrades;
    }

    public void Bind(OfficeData data, bool isFileEmpty) {
        _data = data;
        if (!isFileEmpty)
            _upgradesToBuy = _data.ShopUpgrades.ToList();
        LateStart();
    }
}
