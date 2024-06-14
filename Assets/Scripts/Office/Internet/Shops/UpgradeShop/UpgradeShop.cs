public class UpgradeShop : BaseInstantShop<BaseUpgrade, OfficeData> {
    public override void Bind(OfficeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradesShop = new(_defaultItemsToBuy);
        _data = data.UpgradesShop;
        LateStart();
    }
}
