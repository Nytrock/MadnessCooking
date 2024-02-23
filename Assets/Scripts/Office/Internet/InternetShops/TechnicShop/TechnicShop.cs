using System.Collections.Generic;
using UnityEngine;

public class TechnicShop : BaseInstantShop
{
    [SerializeField] private List<Technic> _technicToBuy;
    [SerializeField] private TechnicManager _technicManager;

    public override void BuyItem(BuyableObject item)
    {
        var technic = item as Technic;
        if (technic == null)
            return;

        MoneyManager.instance.ChangeMoney(-technic.Cost);
        _technicManager.AddTechnic(technic);

        var index = _technicToBuy.IndexOf(technic);
        _technicToBuy.RemoveAt(index);
        _catalog.UpdatePages(index);
        SetObjectsArray();
    }

    protected override void SetObjectsArray()
    {
        _itemsToBuy = _technicToBuy.ToArray();
    }
}
