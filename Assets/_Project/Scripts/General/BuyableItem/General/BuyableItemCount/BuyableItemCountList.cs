using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class BuyableItemCountList<TItem>
    where TItem : BuyableItem {

    [SerializeField, JsonProperty] private List<BuyableItemCount<TItem>> _itemCounts;
    private List<TItem> _availableItems;

    public int Size => _itemCounts.Count;

    public BuyableItemCountList() {
        _itemCounts = new();
        _availableItems = new();
    }

    public BuyableItemCountList(BuyableItemCountList<TItem> itemList) : this() {
        foreach (var item in itemList)
            _itemCounts.Add(new(item));
        UpdateAvailableItems();
    }

    public void Add(BuyableItemCount<TItem> newItemCount) {
        foreach (var itemCount in _itemCounts) {
            if (itemCount.Item == newItemCount.Item) {
                itemCount.AddToCount(itemCount.Count);
                UpdateAvailableItems();
                return;
            }
        }

        _itemCounts.Add(newItemCount);
        UpdateAvailableItems();
    }

    public void Remove(BuyableItemCount<TItem> itemCount) {
        for (int i = 0; i < _itemCounts.Count; i++) {
            if (_itemCounts[i].Item == itemCount.Item) {
                _itemCounts[i].AddToCount(-itemCount.Count);
                if (_itemCounts[i].Count <= 0)
                    _itemCounts.RemoveAt(i);
                break;
            }
        }

        UpdateAvailableItems();
    }

    public void Remove(TItem item, int count) {
        Remove(new(item, count));
    }

    private void UpdateAvailableItems() {
        _availableItems = _itemCounts.Select(count => count.Item).ToList();
    }

    public bool ContainsItem(TItem item) {
        UpdateAvailableItems();
        return _availableItems.Contains(item);
    }

    public bool ContainsCount(BuyableItemCount<TItem> searchingCount) {
        foreach (var countItem in _itemCounts) {
            if (countItem.Item == searchingCount.Item) {
                if (countItem.Count >= searchingCount.Count)
                    return true;
                else
                    return false;
            }
        }

        return false;
    }

    public int IndexOf(BuyableItemCount<TItem> itemCount) {
        return _availableItems.IndexOf(itemCount.Item);
    }

    public void Clear() {
        _itemCounts.Clear();
        _availableItems.Clear();
    }

    public IEnumerator<BuyableItemCount<TItem>> GetEnumerator() {
        foreach (var count in _itemCounts)
            yield return count;
    }

    public IEnumerable<BuyableItemCount<TItem>> GetItems() {
        foreach (var count in _itemCounts)
            yield return count;
    }

    public int GetItemCount(TItem item) {
        foreach (var count in _itemCounts)
            if (count.Item == item)
                return count.Count;
        return 0;
    }
}
