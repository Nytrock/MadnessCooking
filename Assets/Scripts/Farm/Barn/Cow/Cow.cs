public class Cow : NeedHoldAdd, IUpgradeable<FarmUpgradeData> {
    private NeedHoldAddData _flourMillData;
    private FarmUpgradeData _upgradeData;

    public int MaterialCount => _needHoldData.MaterialCount;

    protected override void AddReady() {
        if (!_upgradeData.IsWheatDistributing)
            _flourMillData.SubstractMaterial();
        base.AddReady();
    }

    public override void Bind(FarmData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.Cow = new();

        _holdData = data.Cow;
        _flourMillData = data.FlourMill;
        base.Bind(data, isFileEmpty);
    }

    public void BindUpgrade(FarmUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }
}
