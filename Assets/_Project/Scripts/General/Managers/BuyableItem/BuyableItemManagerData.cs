using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class BuyableItemManagerData<TItem>
    where TItem : BuyableItem {

    [SerializeField, JsonProperty] protected List<TItem> _availableItems = new();

    public IEnumerable<TItem> AvailableItems => _availableItems;
    public int ItemsCount => _availableItems.Count;

    public virtual void AddItem(TItem item) {
        _availableItems.Add(item);
        _availableItems = _availableItems.OrderBy(item => item.Price).ToList();
    }

    public bool IsItemAvailable(TItem item) {
        return _availableItems.Contains(item);
    }
}
