using System;
using UnityEngine;

[Serializable]
public class BuyableItemCount<TItem>
    where TItem : BuyableItem {

    [SerializeField] private TItem _item;
    [SerializeField, Min(1)] private int _count;

    public TItem Item => _item;
    public int Count => _count;

    public event Action<int> CountChanged;

    public BuyableItemCount(TItem item, int count) {
        _item = item;
        _count = count;
    }

    public void ChangeCount(int count) {
        if (_count + count < 0)
            _count = 0;
        else
            _count += count;
        CountChanged?.Invoke(_count);
    }

    public BuyableItemCount<TItem> Copy() {
        return new(_item, _count);
    }
}
