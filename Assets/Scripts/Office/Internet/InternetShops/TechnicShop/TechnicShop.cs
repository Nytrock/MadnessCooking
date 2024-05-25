using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TechnicShop : BaseInstantShop, IBindable<OfficeData>
{
    [SerializeField] private List<Technic> _technicToBuy;
    [SerializeField] private TechnicManager _technicManager;
    private OfficeData _data;

    public override Type Type => typeof(Technic);

    public override void BuyItem(BuyableObject item)
    {
        var technic = item as Technic;
        if (technic == null)
            throw new NullReferenceException($"Buying item is not {Type}");

        MoneyManager.Instance.ChangeMoney(-technic.Cost);
        _technicManager.AddTechnic(technic);

        int index = _technicToBuy.IndexOf(technic);
        _technicToBuy.RemoveAt(index);
        _catalog.RemovePanel(index);
        SetObjectsArray();
    }

    protected override void SetObjectsArray()
    {
        _technicToBuy = _technicToBuy.OrderBy(x => x.Cost).ToList();
        _data.ShopTechnic = _technicToBuy.ToArray();
        _itemsToBuy = _data.ShopTechnic;
    }

    public void Bind(OfficeData data, bool isFileEmpty)
    {
        _data = data;
        if (!isFileEmpty)
            _technicToBuy = _data.ShopTechnic.ToList();
        LateStart();
    }
}
