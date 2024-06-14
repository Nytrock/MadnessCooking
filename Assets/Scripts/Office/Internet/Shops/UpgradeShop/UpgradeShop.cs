using UnityEngine;

public class UpgradeShop : BaseInstantShop<BaseUpgrade, OfficeData> {
    [SerializeField] private UpgradeManager _upgradeManager;

    public override void BuyItem(BaseUpgrade upgrade) {
        _upgradeManager.AddUpgrade(upgrade);
        base.BuyItem(upgrade);
    }

    public override void Bind(OfficeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradesShop = new(_defaultItemsToBuy);
        _data = data.UpgradesShop;
        LateStart();
    }
}
