using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DecorShop : BaseInstantShop, IBindable<OfficeData>
{
    [SerializeField] private List<Decor> _decorToBuy;
    [SerializeField] private Decor _cat;
    [SerializeField] private KitchenDecorManager _kitchenManager;
    [SerializeField] private OfficeDecorManager _officeManager;
    private OfficeData _data;

    public override Type Type => typeof(Decor);

    public override void BuyItem(BuyableObject item)
    {
        var decor = item as Decor;
        if (decor == null)
            return;

        MoneyManager.instance.ChangeMoney(-decor.Cost);
        FatigueManager.instance.AddDecorBonus(decor);
        if (decor.DecorType == DecorType.Kitchen)
            _kitchenManager.AddDecor(decor);
        else if (decor.DecorType == DecorType.Office)
            _officeManager.AddDecor(decor);

        var index = _decorToBuy.IndexOf(decor);
        if (_decorToBuy.Count == 1 && decor != _cat) {
            _decorToBuy[index] = _cat;
            _catalog.UpdatePanel(index, _cat);
        } else {
            _decorToBuy.RemoveAt(index);
            _catalog.RemovePanel(index);
        }

        SetObjectsArray();
    }

    protected override void SetObjectsArray()
    {
        _decorToBuy = _decorToBuy.OrderBy(x => x.Cost).ToList();
        _data.ShopDecor = _decorToBuy.ToArray();
        _itemsToBuy = _data.ShopDecor;
    }

    public void Bind(OfficeData data, bool isFileEmpty)
    {
        _data = data;
        if (!isFileEmpty)
            _decorToBuy = _data.ShopDecor.ToList();
        LateStart();
    }
}
