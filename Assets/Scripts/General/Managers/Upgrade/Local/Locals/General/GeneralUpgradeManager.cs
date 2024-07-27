public class GeneralUpgradeManager : SaveableLocalUpgradeManager<GeneralUpgradeData, GeneralData> {
    public override void Bind(GeneralData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradeData = new();
        _data = data.UpgradeData;
    }
}
