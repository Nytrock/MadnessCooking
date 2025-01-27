using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class ShopData<TItem>
    where TItem : BuyableItem {

    [SerializeField, JsonProperty] private List<TItem> _itemsToBuy;
    [SerializeField, JsonProperty] private List<TItem> _availableItems;

    public IEnumerable<TItem> ItemsToBuy => _itemsToBuy;

    public ShopData(IEnumerable<TItem> defaultItems) {
        _itemsToBuy = new();
        _availableItems = new();

        if (defaultItems == null)
            return;

        foreach (var item in defaultItems)
            _itemsToBuy.Add(item);
    }

    public void CheckDefaultItems(IEnumerable<TItem> defaultItems) {
        foreach (var item in defaultItems)
            if (!IsItemAvailable(item) && !IsItemBuyable(item))
                _itemsToBuy.Add(item);

        CheckAllItems();
    }

    private void CheckAllItems() {
        foreach (var item in _availableItems) {
            if ((item as IGraphable<TItem>) != null)
                CheckItemGraph(item);
        }
    }

    private void CheckItemGraph(TItem item) {
        IGraphable<TItem> graphable = item as IGraphable<TItem>;
        if (graphable == null)
            return;

        foreach (var nextItem in graphable.NextItems) {
            if (IsItemBuyable(nextItem))
                continue;

            if (IsItemAvailable(nextItem)) {
                CheckItemGraph(nextItem);
                continue;
            }

            bool canAdd = true;
            if (nextItem is IGraphable<TItem> graphNextItem)
                foreach (var needUpgrade in graphNextItem.NeedItems)
                    canAdd &= IsItemAvailable(needUpgrade);

            if (canAdd)
                _itemsToBuy.Add(nextItem);
        }
    }

    public void AddItemToBuy(TItem item) {
        if (_itemsToBuy.Contains(item) || _availableItems.Contains(item))
            return;

        _itemsToBuy.Add(item);
    }

    public void ReplaceItemToBuy(TItem item, int index) {
        if (index < 0 || index >= _itemsToBuy.Count)
            return;

        _itemsToBuy[index] = item;
    }

    public void RemoveItemByIndex(int index) {
        if (index < 0 || index >= _itemsToBuy.Count)
            return;
        _itemsToBuy.RemoveAt(index);
    }

    public void BuyItem(TItem item) {
        if (_availableItems.Contains(item))
            return;

        _availableItems.Add(item);
    }

    public bool IsItemAvailable(TItem item) {
        return _availableItems.Contains(item);
    }

    public int IndexOfItem(TItem item) {
        return _itemsToBuy.IndexOf(item);
    }

    public bool IsItemBuyable(TItem item) {
        return _itemsToBuy.Contains(item);
    }

    public void OrderItems(Func<TItem, int> sortMethod) {
        _itemsToBuy = _itemsToBuy.OrderBy(sortMethod).ToList();
    }

    public TItem GetItem(int index) {
        return _itemsToBuy[index];
    }
}
