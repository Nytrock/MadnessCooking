using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DecorShop : BaseInstantShop, IBindable<OfficeData> {
    [SerializeField] private List<Decor> _decorToBuy;
    [SerializeField] private BaseDecorManager[] _decorManagers;
    private OfficeData _data;

    public override Type Type => typeof(Decor);

    public override void BuyItem(BuyableObject item) {
        var decor = item as Decor;
        if (decor == null)
            throw new NullReferenceException($"Buying item is not {Type}");

        MoneyManager.Instance.ChangeMoney(-decor.Price);
        FatigueManager.Instance.AddDecorBonus(decor);
        _data.AvailableDecor.Add(decor);
        foreach (var decorManager in _decorManagers)
            decorManager.AddDecor(decor);

        int index = _decorToBuy.IndexOf(decor);
        bool isFirst = true;
        CheckNextDecor(decor, index, ref isFirst);

        SetObjectsArray();
    }

    private void CheckNextDecor(Decor decor, int index, ref bool isFirst) {
        foreach (var nextDecor in decor.NextItems) {
            if (_data.AvailableDecor.Contains(nextDecor) || _decorToBuy.Contains(nextDecor)) {
                CheckNextDecor(nextDecor, index, ref isFirst);
                continue;
            }
            bool canAdd = true;
            foreach (var needUpgrade in nextDecor.NeedItems)
                canAdd &= _data.AvailableDecor.Contains(needUpgrade);
            if (canAdd) {
                if (isFirst) {
                    _decorToBuy[index] = nextDecor;
                    _catalog.UpdatePanel(index, nextDecor);
                    isFirst = false;
                } else {
                    _decorToBuy.Add(nextDecor);
                    _catalog.GeneratePanel(nextDecor);
                }
            }
        }

        if (isFirst) {
            _decorToBuy.RemoveAt(index);
            _catalog.RemovePanel(index);
        }
    }

    protected override void SetObjectsArray() {
        _decorToBuy = _decorToBuy.OrderBy(x => x.Price).ToList();
        _data.ShopDecor = _decorToBuy.ToArray();
        _itemsToBuy = _data.ShopDecor;
    }

    public void Bind(OfficeData data, bool isFileEmpty) {
        _data = data;
        if (!isFileEmpty)
            _decorToBuy = _data.ShopDecor.ToList();
        LateStart();
    }
}
