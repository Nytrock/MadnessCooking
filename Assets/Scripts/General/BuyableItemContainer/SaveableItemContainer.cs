public abstract class SaveableItemContainer<TItem, TData> : BuyableItemContainer<TItem>, IBindable<TData>
    where TItem : BuyableItem where TData : ISaveable {

    public abstract void Bind(TData data, bool isFileEmpty);
}
