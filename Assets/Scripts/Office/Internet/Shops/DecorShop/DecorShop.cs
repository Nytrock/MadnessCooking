public class DecorShop : BaseInstantShop<Decor, OfficeData> {
    public override void Bind(OfficeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.DecorShop = new(_defaultItemsToBuy);
        _data = data.DecorShop;
        base.Bind(data, isFileEmpty);
    }
}
