public interface IUpgradeable<TData>
    where TData : LocalUpgradeData {

    void BindUpgrade(TData upgradeData);
    void CheckAddedUpgrade(BaseUpgrade upgrade);
}
