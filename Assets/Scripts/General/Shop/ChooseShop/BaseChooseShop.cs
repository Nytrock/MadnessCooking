using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseChooseShop<TItem, TData> : SaveableBaseShop<TItem, TData>
    where TItem : BuyableItem where TData : ISaveable {

    [SerializeField] protected BaseChooseShopItemView<TItem> _itemView;
    protected TItem _itemToBuy;

    public event Action<TItem> ItemSelected;

    protected virtual void Awake() {
        _itemView.SetButtonAction(delegate { BuyItem(_itemToBuy); });
    }

    public override void ChangeShopState(bool newState) {
        base.ChangeShopState(newState);

        if (!newState)
            _itemView.ResetInfo();
    }

    protected override void UpdateItem(TItem newItem, int index) {
        base.UpdateItem(newItem, index);
        _itemToBuy = newItem;
        _itemView.UpdateItem(newItem, IsBuyable(newItem));
    }

    protected void ChooseItem(TItem item) {
        if (_itemToBuy == item)
            _itemToBuy = null;
        else
            _itemToBuy = item;

        ItemSelected?.Invoke(_itemToBuy);
        _renderer.UpdateSelectedItem(_itemToBuy);
        _itemView.UpdateItem(_itemToBuy, IsBuyable(_itemToBuy));
    }

    protected override void UpdatePanels() {
        base.UpdatePanels();
        if (_itemToBuy != null)
            _itemView.UpdateBuyable(IsBuyable(_itemToBuy));
    }

    protected override void RemoveItem(TItem item, int index) {
        base.RemoveItem(item, index);
        _itemView.ResetInfo();
    }

    protected override UnityAction GetPanelAction(BuyableItem item) {
        return () => ChooseItem(item as TItem);
    }
}