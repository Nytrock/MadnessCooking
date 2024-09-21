public abstract class SaveableItemManager<TItem, TData> : BuyableItemManager<TItem>, IBindable<TData>
    where TItem : BuyableItem where TData : ISaveable {

    public virtual void Bind(TData data) {
        foreach (var item in _defaultItems)
            AddItem(item);
    }
}
