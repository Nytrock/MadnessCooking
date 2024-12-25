using UnityEngine;

public class UpgradeShop : BaseInstantShop<BaseUpgrade, OfficeData> {
    [SerializeField] private UpgradeTypeSprite[] _upgradeTypes;

    public override void Bind(OfficeData data) {
        data.UpgradesShop ??= new(_defaultItemsToBuy);
        _data = data.UpgradesShop;
    }

    protected override GrayscaleImageData GenerateSideInfo(BaseUpgrade upgrade) {
        foreach (var type in _upgradeTypes)
            if (type.Type == upgrade.Type)
                return new(type.Sprite, false);
        return null;
    }
}
