using UnityEngine;

public class TechnicShop : BaseInstantShop<Technic, OfficeData> {
    [SerializeField] private TechnicManager _technicManager;

    public override void BuyItem(Technic technic) {
        _technicManager.AddTechnic(technic);
        base.BuyItem(technic);
    }

    public override void Bind(OfficeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.TechnicShop = new(_defaultItemsToBuy);
        _data = data.TechnicShop;
        base.Bind(data, isFileEmpty);
    }
}
