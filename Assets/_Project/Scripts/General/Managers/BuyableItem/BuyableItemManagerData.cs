using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class BuyableItemManagerData<TItem>
    where TItem : BuyableItem {

    [SerializeField, JsonProperty] private List<TItem> _availableItems;

    public IEnumerable<TItem> AvailableItems => _availableItems;
    public int ItemsCount => _availableItems.Count;

    public BuyableItemManagerData() {
        _availableItems = new();
    }

    public void AddItem(TItem item) {
        if (_availableItems.Contains(item))
            return;

        _availableItems.Add(item);
        _availableItems = _availableItems.OrderBy(item => item.Price).ToList();
    }

    public TItem GetItem(int index) {
        return _availableItems[index];
    }

    public bool IsItemAvailable(TItem item) {
        return _availableItems.Contains(item);
    }
}
