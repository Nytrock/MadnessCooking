using System;

public class DecorShop : BaseInstantShop<Decor, OfficeData> {

    protected override void SortItems() {
        Func<Decor, int> sortMethod = (decor) => decor.Price + ((int)decor.Location * 10000);
        _data.OrderItems(sortMethod);
    }

    public override void Bind(OfficeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.DecorShop = new(_defaultItemsToBuy);
        _data = data.DecorShop;
        base.Bind(data, isFileEmpty);
    }
}
