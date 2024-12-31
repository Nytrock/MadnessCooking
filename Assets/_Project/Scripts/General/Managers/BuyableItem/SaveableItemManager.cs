public abstract class SaveableItemManager<TItem, TData> : BuyableItemManager<TItem>, IBindable<TData>
    where TItem : BuyableItem where TData : ISaveable {

    public void LateStart() {
        foreach (var item in _data.AvailableItems)
            InvokeItemAdded(item);

        foreach (var item in _defaultItems)
            AddItem(item);
    }

    public abstract void Bind(TData data);
}
