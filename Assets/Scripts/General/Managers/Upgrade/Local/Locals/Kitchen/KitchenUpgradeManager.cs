public class KitchenUpgradeManager : SaveableLocalUpgradeManager<KitchenUpgradeData, KitchenData> {
    public override void Bind(KitchenData data) {
        data.UpgradeData ??= new();
        _data = data.UpgradeData;
    }
}
