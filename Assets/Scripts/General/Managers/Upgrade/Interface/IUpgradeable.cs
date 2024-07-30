public interface IUpgradeable<TData>
    where TData : ISaveable {

    void BindUpgrade(TData upgradeData);
    void CheckAddedUpgrade(BaseUpgrade upgrade);
}
