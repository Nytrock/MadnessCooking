public class SinkUpgradeRenderer : UpgradeRenderer, IUpgradeable<KitchenUpgradeData> {
    private KitchenUpgradeData _upgradeData;

    public void BindUpgrade(KitchenUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    protected override void ChangeState(bool newState) {
        base.ChangeState(newState);
        if (newState)
            _upgradeData.ChangeWaterAvailable();
    }
}
