using UnityEngine.Events;

public abstract class BaseInstantShop<TItem, TData> : SaveableBaseShop<TItem, TData>
    where TItem : BuyableObject where TData : ISaveable {

    protected override UnityAction GetPanelAction(BuyableObject item) {
        return () => BuyItem(item as TItem);
    }
}
