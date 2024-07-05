using System;
using UnityEngine;

[Serializable]
public class BuyableItemCount<TItem>
    where TItem : BuyableItem {

    [SerializeField] private TItem _item;
    [SerializeField, Min(1)] private int _count;

    public TItem Item => _item;
    public int Count => _count;

    public BuyableItemCount(TItem ingredient, int count) {
        _item = ingredient;
        _count = count;
    }

    public void ChangeCount(int count) {
        if (_count + count < 0)
            _count = 0;
        else
            _count += count;
    }
}
