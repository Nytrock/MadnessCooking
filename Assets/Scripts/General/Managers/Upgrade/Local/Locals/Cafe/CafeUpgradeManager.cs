public class CafeUpgradeManager : SaveableLocalUpgradeManager<CafeUpgradeData, CafeData> {
    public override void Bind(CafeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradeData = new();
        _data = data.UpgradeData;
    }
}
