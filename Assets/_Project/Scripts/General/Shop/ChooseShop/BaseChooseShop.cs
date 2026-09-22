using System;
using UnityEngine;
using UnityEngine.Events;

namespace MadnessCooking.General {
    public abstract class BaseChooseShop<TItem> : BuyableItemShop<TItem> where TItem : BuyableItem {
        [SerializeField] protected BaseChooseShopItemView<TItem> _itemView;
        protected TItem _itemToBuy;
        protected BaseChooseBuyPanel _choosedPanel;

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

        public override void BuyItem(TItem item) {
            TItem tempItem = _itemToBuy;
            base.BuyItem(item);

            if (tempItem == _itemToBuy)
                _itemToBuy = null;
        }

        protected void ChooseItem(TItem item) {
            int itemIndex = _data.IndexOfItem(item);
            BaseChooseBuyPanel buyPanel = _renderer.GetPanelByIndex(itemIndex) as BaseChooseBuyPanel;

            if (_itemToBuy == item) {
                _itemToBuy = null;
                _choosedPanel.UpdateSelection(false);
                _choosedPanel = null;
            } else {
                _itemToBuy = item;
                if (_choosedPanel != null)
                    _choosedPanel.UpdateSelection(false);
                _choosedPanel = buyPanel;
                _choosedPanel.UpdateSelection(true);
            }

            _itemView.UpdateItem(_itemToBuy, IsBuyable(_itemToBuy));
            ItemSelected?.Invoke(_itemToBuy);
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
}