using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuyableItemManager<TItem> : MonoBehaviour
    where TItem : BuyableItem {

    [SerializeField] protected List<TItem> _defaultItems;
    [SerializeField] protected BuyableItemManagerData<TItem> _data = new();

    public event Action<TItem> ItemAdded;

    public virtual void AddItem(TItem item) {
        _data.AddItem(item);
        InvokeItemAdded(item);
    }

    protected void InvokeItemAdded(TItem item) {
        ItemAdded?.Invoke(item);
    }
}
