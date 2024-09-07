using UnityEngine;

public class FarmShop : BaseChooseShop<BaseUpgrade, FarmData> {
    [SerializeField] private ConsumableUpgrade[] _defaultConsumableUpgrades;

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

    public override void Bind(FarmData data) {
        data.FarmShop ??= new(_defaultItemsToBuy, _defaultConsumableUpgrades);
        _data = data.FarmShop;

        base.Bind(data);
    }
}
