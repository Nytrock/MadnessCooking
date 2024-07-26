public class KitchenUpgradeManager : LocalUpgradeManager<KitchenUpgradeData, KitchenData> {
    public override void Bind(KitchenData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradeData = new();
        _data = data.UpgradeData;
    }
}
