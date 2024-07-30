using System;
using UnityEngine;

public class UpgradeShop : BaseInstantShop<BaseUpgrade, OfficeData> {
    [SerializeField] private UpgradeTypeImage[] _upgradeTypes;

    protected override void SortItems() {
        Func<BaseUpgrade, int> sortMethod = (upgrade) => upgrade.Price + ((int)upgrade.Type * 10000);
        _data.OrderItems(sortMethod);
    }

    public override void Bind(OfficeData data) {
        data.UpgradesShop ??= new(_defaultItemsToBuy);
        _data = data.UpgradesShop;
        LateStart();
    }

    protected override GrayscaleImageData GenerateSideInfo(BaseUpgrade upgrade) {
        foreach (var type in _upgradeTypes)
            if (type.Type == upgrade.Type)
                return new(type.Sprite, false);
        return null;
    }
}
