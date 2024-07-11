using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ShopData<TItem>
    where TItem : BuyableItem {

    [SerializeField] private List<TItem> _itemsToBuy;

    public IEnumerable<TItem> ItemsToBuy => _itemsToBuy;

    public ShopData(IEnumerable<TItem> defaultItems) {
        _itemsToBuy = new();
        foreach (var item in defaultItems)
            _itemsToBuy.Add(item);
    }

    public void AddItemToBuy(TItem item) {
        _itemsToBuy.Add(item);
    }

    public void ReplaceItemToBuy(TItem item, int index) {
        if (index < 0 || index >= _itemsToBuy.Count)
            return;

        _itemsToBuy[index] = item;
    }

    public void RemoveItemToBuy(int index) {
        if (index < 0 || index >= _itemsToBuy.Count)
            return;

        _itemsToBuy.RemoveAt(index);
    }

    public int IndexOfItemPanel(TItem item) {
        return _itemsToBuy.IndexOf(item);
    }

    public bool IsItemBuyable(TItem item) {
        return _itemsToBuy.Contains(item);
    }

    public TItem GetItemToBuy(int index) {
        return _itemsToBuy[index];
    }

    public void OrderItems(Func<TItem, int> sortMethod) {
        _itemsToBuy = _itemsToBuy.OrderBy(sortMethod).ToList();
    }
}
