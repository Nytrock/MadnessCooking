using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuyableItemManagerData<TItem>
    where TItem : BuyableItem {

    [SerializeField] private List<TItem> _availableItems;

    public IEnumerable<TItem> AvailableItems => _availableItems;
    public int ItemsCount => _availableItems.Count;

    public BuyableItemManagerData() {
        _availableItems = new();
    }

    public void AddItem(TItem item) {
        if (_availableItems.Contains(item))
            return;

        _availableItems.Add(item);
    }

    public TItem GetItem(int index) {
        return _availableItems[index];
    }

    public bool IsItemAvailable(TItem item) {
        return _availableItems.Contains(item);
    }
}
