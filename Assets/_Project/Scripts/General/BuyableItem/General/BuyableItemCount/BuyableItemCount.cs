using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class BuyableItemCount<TItem>
    where TItem : BuyableItem {

    [SerializeField, JsonProperty] private TItem _item;
    [SerializeField, Min(1), JsonProperty] private int _count;

    public TItem Item => _item;
    public int Count => _count;

    public event Action<int> CountChanged;

    public BuyableItemCount(TItem item, int count) {
        _item = item;
        _count = count;
    }

    public BuyableItemCount(BuyableItemCount<TItem> buyableItemCount) {
        _item = buyableItemCount._item;
        _count = buyableItemCount._count;
    }

    public void ChangeCount(int count) {
        if (_count + count < 0)
            _count = 0;
        else
            _count += count;
        CountChanged?.Invoke(_count);
    }
}
