namespace MadnessCooking.General {
    public class OfficeUpgradeManager : SaveableLocalUpgradeManager<OfficeUpgradeData, OfficeData> {
        public override void Bind(OfficeData data) {
            data.UpgradeData ??= new();
            _data = data.UpgradeData;
        }
    }
}
