using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class BuyableItemCountList<TItem>
    where TItem : BuyableItem {

    [SerializeField, JsonProperty] private List<BuyableItemCount<TItem>> _itemCounts = new();
    private List<TItem> _availableItems = new();

    public int Size => _itemCounts.Count;

    public void Add(BuyableItemCount<TItem> itemCount) {
        if (ContainsItem(itemCount))
            _itemCounts[IndexOf(itemCount)].ChangeCount(itemCount.Count);
        else
            _itemCounts.Add(itemCount);

        UpdateAvailableItems();
    }

    public void Remove(BuyableItemCount<TItem> itemCount) {
        if (!ContainsItem(itemCount))
            return;

        int index = IndexOf(itemCount);
        _itemCounts[index].ChangeCount(-itemCount.Count);
        if (_itemCounts[index].Count <= 0)
            _itemCounts.RemoveAt(index);

        UpdateAvailableItems();
    }

    public void Remove(TItem item, int count) {
        Remove(new(item, count));
    }

    private void UpdateAvailableItems() {
        _availableItems = _itemCounts.Select(count => count.Item).ToList();
    }

    public bool ContainsItem(BuyableItemCount<TItem> itemCount) {
        return _availableItems.Contains(itemCount.Item);
    }

    public bool ContainsCount(BuyableItemCount<TItem> itemCount) {
        if (!ContainsItem(itemCount))
            return false;
        return _itemCounts[IndexOf(itemCount)].Count >= itemCount.Count;
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
