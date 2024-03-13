using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DecorShop : BaseInstantShop
{
    [SerializeField] private List<Decor> _decorToBuy;
    [SerializeField] private Decor _cat;
    [SerializeField] private DecorManager _kitchenManager;
    [SerializeField] private DecorManager _officeManager;

    public override void BuyItem(BuyableObject item)
    {
        var decor = item as Decor;
        if (decor == null)
            return;

        MoneyManager.instance.ChangeMoney(-decor.Cost);
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
        _itemsToBuy = _decorToBuy.ToArray();
    }
}
