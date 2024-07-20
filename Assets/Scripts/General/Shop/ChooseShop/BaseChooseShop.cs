using UnityEngine;
using UnityEngine.Events;

public abstract class BaseChooseShop<TItem, TData> : SaveableBaseShop<TItem, TData>
    where TItem : BuyableItem where TData : ISaveable {

    [SerializeField] protected BaseChooseShopItemView<TItem> _itemView;
    private TItem _itemToBuy;

    protected virtual void Awake() {
        _itemView.SetButtonAction(delegate { BuyItem(_itemToBuy); });
    }

    public override void ChangeShopState(bool newState) {
        base.ChangeShopState(newState);

        if (!newState)
            _itemView.ResetInfo();
    }

    protected override void ReplaceItemPanel(TItem newItem, int index) {
        base.ReplaceItemPanel(newItem, index);
        _itemToBuy = newItem;
        _itemView.ShowItem(newItem, IsBuyable(newItem));
    }

    private void ChooseItem(TItem item) {
        _itemToBuy = item;
        _itemView.ShowItem(_itemToBuy, IsBuyable(_itemToBuy));
    }

    protected override void UpdatePanels() {
        base.UpdatePanels();
        if (_itemToBuy != null)
            _itemView.UpdateBuyable(IsBuyable(_itemToBuy));
    }

    protected override void RemoveItemPanel(TItem item, int index) {
        base.RemoveItemPanel(item, index);
        _itemView.ResetInfo();
    }

    protected override UnityAction GetPanelAction(BuyableItem item) {
        return () => ChooseItem(item as TItem);
    }
}