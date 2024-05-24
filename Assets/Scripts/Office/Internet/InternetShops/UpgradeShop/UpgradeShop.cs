using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeShop : BaseInstantShop, IBindable<OfficeData>
{
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private List<BaseUpgrade> _upgradesToBuy;
    private List<BaseUpgrade> _availableUpgrades = new();
    private OfficeData _data;

    public override Type Type => typeof(BaseUpgrade);

    public override void BuyItem(BuyableObject item)
    {
        var upgrade = item as BaseUpgrade;
        if (upgrade == null)
            throw new NullReferenceException($"Buying item is not {Type}");

        MoneyManager.instance.ChangeMoney(-upgrade.Cost);
        _upgradeManager.NewUpgrade(upgrade);
        _availableUpgrades.Add(upgrade);

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

    private void CheckNextUpgrades(GraphUpgrade graphUpgrade, int index, ref bool isFirst)
    {
        foreach (var nextUpgrade in graphUpgrade.NextUpgrades) {
            if (_availableUpgrades.Contains(nextUpgrade) || _upgradesToBuy.Contains(nextUpgrade)) {
                CheckNextUpgrades(nextUpgrade, index, ref isFirst);
                continue;
            }
            bool canAdd = true;
            foreach (var needUpgrade in nextUpgrade.NeedUpgrades)
                canAdd &= _availableUpgrades.Contains(needUpgrade);
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
        _data.ShopUpgrades = _upgradesToBuy.ToArray();
        _itemsToBuy = _data.ShopUpgrades;
    }

    public void Bind(OfficeData data, bool isFileEmpty)
    {
        _data = data;
        if (!isFileEmpty)
            _upgradesToBuy = _data.ShopUpgrades.ToList();
        LateStart();
    }
}
