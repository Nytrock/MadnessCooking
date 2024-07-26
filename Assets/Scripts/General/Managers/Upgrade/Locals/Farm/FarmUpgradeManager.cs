public class FarmUpgradeManager : LocalUpgradeManager<FarmUpgradeData, FarmData> {
    public override void Bind(FarmData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradeData = new();
        _data = data.UpgradeData;
    }
}
