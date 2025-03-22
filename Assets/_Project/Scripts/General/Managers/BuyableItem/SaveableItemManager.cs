public abstract class SaveableItemManager<TItem, TData> : BuyableItemManager<TItem>, IBindable<TData>
    where TItem : BuyableItem where TData : ISaveable {

    public virtual void LateStart() {
        foreach (var item in _data.AvailableItems)
            InvokeItemAdded(item);
    }

    public abstract void Bind(TData data);
}
