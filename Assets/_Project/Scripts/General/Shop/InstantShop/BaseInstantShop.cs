using UnityEngine.Events;

namespace MadnessCooking.General {
    public abstract class BaseInstantShop<TItem> : BuyableItemShop<TItem> where TItem : BuyableItem {
        protected override UnityAction GetPanelAction(BuyableItem item) {
            return () => BuyItem(item as TItem);
        }
    }
}
