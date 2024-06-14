using UnityEngine;

public class FarmShop : BaseChooseShop<BaseUpgrade, FarmData> {
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private ConsumableUpgrade[] _defaultConsumableUpgrades;

    private FarmShopData _specialData => _data as FarmShopData;

    public override void BuyItem(BaseUpgrade item) {
        base.BuyItem(item);
        _upgradeManager.AddUpgrade(item);
    }

    protected override void ChangePanelsState(BaseUpgrade upgrade) {
        int index = _data.IndexOfItemPanel(upgrade);
        if (upgrade as ConsumableUpgrade) {
            var consumableUpgrade = upgrade as ConsumableUpgrade;
            bool isMax = _specialData.AddCountAndCheckMax(consumableUpgrade);
            if (isMax)
                CheckGraph(consumableUpgrade, index);
        } else {
            base.ChangePanelsState(upgrade);
        }
    }

    public override void Bind(FarmData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.FarmShop = new(_defaultItemsToBuy, _defaultConsumableUpgrades);
        _data = data.FarmShop;

        base.Bind(data, isFileEmpty);
    }
}
