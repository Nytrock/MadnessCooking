using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuyableItemManager<TItem> : MonoBehaviour
    where TItem : BuyableItem {

    [SerializeField] protected List<TItem> _defaultItems;
    [SerializeField] protected List<TItem> _allItems;

    protected BuyableItemManagerData<TItem> _data;

    public int AllItemsCount => _allItems.Count;
    public int AvailableItemsCount => _data.ItemsCount;

    public event Action<TItem> ItemAdded;

    public virtual void AddItem(TItem item) {
        if (_data.IsItemAvailable(item))
            return;

        _data.AddItem(item);
        InvokeItemAdded(item);
    }

    protected void InvokeItemAdded(TItem item) {
        ItemAdded?.Invoke(item);
    }

    public IEnumerable<TItem> GetAllItems() {
        foreach (var item in _allItems)
            yield return item;
    }

    public virtual bool IsItemAvailable(TItem item) {
        return _data.IsItemAvailable(item);
    }
}
