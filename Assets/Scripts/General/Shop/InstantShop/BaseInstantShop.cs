using UnityEngine.Events;

public abstract class BaseInstantShop<TItem, TData> : SaveableBaseShop<TItem, TData>
    where TItem : BuyableItem where TData : ISaveable {

    protected override UnityAction GetPanelAction(BuyableItem item) {
        return () => BuyItem(item as TItem);
    }
}
