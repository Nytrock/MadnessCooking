using UnityEngine;

public class FarmShop : BaseChooseShop<BaseUpgrade, FarmData> {
    [SerializeField] private ConsumableUpgrade[] _defaultConsumableUpgrades;
    [SerializeField] private LocationActivator _shopActivator;

    private FarmShopData _specialData => _data as FarmShopData;

    private void Start() {
        ChangeShopState(true);
    }

    protected override void UpdateItemsAfterBuying(BaseUpgrade upgrade) {
        int index = _data.IndexOfItem(upgrade);
        if (upgrade as ConsumableUpgrade) {
            var consumableUpgrade = upgrade as ConsumableUpgrade;
            bool isMax = _specialData.AddCountAndCheckMax(consumableUpgrade);
            if (isMax)
                CheckGraph(consumableUpgrade, index);
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
