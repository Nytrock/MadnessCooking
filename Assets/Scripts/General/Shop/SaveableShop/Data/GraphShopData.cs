using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GraphShopData<TItem> : ShopData<TItem>
    where TItem : BuyableItem {

    [SerializeField] private List<TItem> _availableItems;

    public GraphShopData(IEnumerable<TItem> defaultItems) : base(defaultItems) {
        _availableItems = new();
    }

    public void BuyItem(TItem item) {
        _availableItems.Add(item);
    }

    public bool IsItemAvailable(TItem item) {
        return _availableItems.Contains(item);
    }
}
