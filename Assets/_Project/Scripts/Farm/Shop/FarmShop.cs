using System;
using UnityEngine;

public class FarmShop : BaseChooseShop<BaseUpgrade, FarmData> {
    [SerializeField] private ConsumableUpgrade[] _defaultConsumableUpgrades;
    [SerializeField] private LocationActivator _shopActivator;

    private FarmShopData _specialData => _data as FarmShopData;

    public event Action<ConsumableUpgrade> ConsumableUpgradeBuyed;

    public override void LateStart() {
        base.LateStart();
        ChangeShopState(true);
    }

    public override void BuyItem(BaseUpgrade item) {
        BaseUpgrade upgrade = _itemToBuy;
        base.BuyItem(item);

        if (upgrade as ConsumableUpgrade && !_specialData.IsConsumableMax(upgrade as ConsumableUpgrade))
            _itemToBuy = upgrade;
    }

    protected override void UpdateItemsAfterBuying(BaseUpgrade upgrade) {
        int index = _data.IndexOfItem(upgrade);
        Debug.Log(upgrade);
        if (upgrade as ConsumableUpgrade) {
            var consumableUpgrade = upgrade as ConsumableUpgrade;
            bool isMax = _specialData.AddConsumableAndCheckMax(consumableUpgrade);
            if (isMax)
                CheckGraph(consumableUpgrade, index);
            ConsumableUpgradeBuyed?.Invoke(consumableUpgrade);
        } else {
            base.UpdateItemsAfterBuying(upgrade);
        }
    }

    public void CloseShop() {
        if (_itemToBuy != null)
            ChooseItem(_itemToBuy);

        _shopActivator.ChangeLocation();
    }

    public override void Bind(FarmData data) {
        data.FarmShop ??= new(_defaultItemsToBuy, _defaultConsumableUpgrades);
        _data = data.FarmShop;

        base.Bind(data);
    }
}
