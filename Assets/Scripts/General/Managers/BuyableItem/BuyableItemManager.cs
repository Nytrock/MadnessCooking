using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuyableItemManager<TItem> : MonoBehaviour
    where TItem : BuyableItem {

    [SerializeField] protected List<TItem> _defaultItems;
    protected BuyableItemManagerData<TItem> _data;

    public event Action<TItem> ItemAdded;

    public virtual void AddItem(TItem item) {
        _data.AddItem(item);
        ItemAdded?.Invoke(item);
    }
}
