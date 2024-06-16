public abstract class SaveableItemManager<TItem, TData> : BuyableItemManager<TItem>, IBindable<TData>
    where TItem : BuyableItem where TData : ISaveable {

    public abstract void Bind(TData data, bool isFileEmpty);
}
