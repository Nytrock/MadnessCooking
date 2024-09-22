public class OfficeBedUpgradeRenderer : UpgradeRenderer, IUpgradeable<OfficeUpgradeData> {
    private OfficeUpgradeData _upgradeData;

    public void BindUpgrade(OfficeUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    protected override void ChangeState(bool newState) {
        base.ChangeState(newState);
        if (newState)
            _upgradeData.ChangeSleepCoef(_upgrade as CoefficientUpgrade);
    }

}
