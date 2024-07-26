public class OfficeUpgradeManager : LocalUpgradeManager<OfficeUpgradeData, OfficeData> {
    public override void Bind(OfficeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradeData = new();
        _data = data.UpgradeData;
    }
}
