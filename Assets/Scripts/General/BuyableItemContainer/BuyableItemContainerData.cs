using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuyableItemContainerData<TItem>
    where TItem : BuyableItem {

    [SerializeField] private List<TItem> _availableItems;

    public IEnumerable<TItem> AvailableItems => _availableItems;
    public int ItemsCount => _availableItems.Count;

    public BuyableItemContainerData(IEnumerable<TItem> defaultItems) {
        _availableItems = new();
        foreach (var item in defaultItems)
            _availableItems.Add(item);
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
