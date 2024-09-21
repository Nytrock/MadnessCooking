public class FarmUpgradeManager : SaveableLocalUpgradeManager<FarmUpgradeData, FarmData> {
    public override void Bind(FarmData data) {
        data.UpgradeData ??= new();
        _data = data.UpgradeData;
    }
}
