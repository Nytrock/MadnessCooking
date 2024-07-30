public class TechnicShop : BaseInstantShop<Technic, OfficeData> {
    public override void Bind(OfficeData data) {
        data.TechnicShop ??= new(_defaultItemsToBuy);
        _data = data.TechnicShop;
        base.Bind(data);
    }
}
