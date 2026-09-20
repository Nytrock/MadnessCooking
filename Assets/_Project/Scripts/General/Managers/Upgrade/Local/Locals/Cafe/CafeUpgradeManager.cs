namespace MadnessCooking.General {
    public class CafeUpgradeManager : SaveableLocalUpgradeManager<CafeUpgradeData, CafeData> {
        public override void Bind(CafeData data) {
            data.UpgradeData ??= new();
            _data = data.UpgradeData;
        }
    }
}
