public class GeneralUpgradeManager : SaveableLocalUpgradeManager<GeneralUpgradeData, GeneralData> {
    public override void Bind(GeneralData data) {
        data.UpgradeData ??= new();
        _data = data.UpgradeData;
    }
}
