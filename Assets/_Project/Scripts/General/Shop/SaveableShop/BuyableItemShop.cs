using System;
using UnityEngine;

namespace MadnessCooking.General {
    public abstract class BuyableItemShop<TItem> : BaseShop, ISaveable where TItem : BuyableItem {
        [SerializeField] protected TItem[] _defaultItemsToBuy;
        [SerializeField] protected BuyableItemManager<TItem> _itemManager;
        protected ShopData<TItem> _data;

        public event Action<BuyableItem> ItemBought;

        public virtual void LateStart() {
            ChangeShopState(false);
            SortItems();
            GenerateShop();
        }

        public virtual void GenerateShop() {
            foreach (var item in _data.ItemsToBuy)
                _renderer.AddPanel(GetPanelData(item));
        }

        private BuyPanelData GetPanelData(TItem item) {
            return new BuyPanelData(item, IsBuyable(item), GetPanelAction(item), GenerateSideInfo(item));
        }

        protected virtual void UpdatePanels() {
            int index = 0;
            foreach (var item in _data.ItemsToBuy) {
                _renderer.UpdatePanel(index, GetPanelData(item));
                index++;
            }
        }

        public override void TryToBuyItem(BuyableItem item) {
            if (item as TItem == null)
                return;

            BuyItem(item as TItem);
        }

        public virtual void BuyItem(TItem item) {
            MoneyManager.Instance.ChangeMoney(-item.Price);
            _data.BuyItem(item);
            _itemManager.AddItem(item);
            UpdateItemsAfterBuying(item);
            ItemBought?.Invoke(item);
        }

        protected virtual void UpdateItemsAfterBuying(TItem bougthItem) {
            int index = _data.IndexOfItem(bougthItem);
            if ((bougthItem as IGraphable<TItem>) != null)
                CheckGraph(bougthItem, index);
            else
                RemoveItem(bougthItem, index);
        }

        protected virtual void RemoveItem(TItem item, int index) {
            _data.RemoveItemByIndex(index);
            _renderer.RemovePanel(index);
        }

        protected virtual void UpdateItem(TItem newItem, int index) {
            _data.ReplaceItemToBuy(newItem, index);
            _renderer.UpdatePanel(index, GetPanelData(newItem));
        }

        protected void AddItem(TItem newItem) {
            _data.AddItemToBuy(newItem);
            _renderer.AddPanel(GetPanelData(newItem));
        }

        protected virtual void CheckGraph(TItem item, int index) {
            bool isFirstReplaced = false;
            CheckNextItems(item, index, ref isFirstReplaced);

            if (!isFirstReplaced)
                RemoveItem(item, index);
        }

        protected void CheckNextItems(TItem item, int index, ref bool isFirstReplaced) {
            if (item is not IGraphable<TItem> graphItem)
                return;

            foreach (var nextItem in graphItem.NextItems) {
                if (_data.IsItemBuyable(nextItem))
                    continue;

                if (_data.IsItemAvailable(nextItem)) {
                    CheckNextItems(nextItem, index, ref isFirstReplaced);
                    continue;
                }

                bool canAdd = true;
                if (nextItem is IGraphable<TItem> graphNextItem)
                    foreach (var needUpgrade in graphNextItem.NeedItems)
                        canAdd &= _data.IsItemAvailable(needUpgrade);

                if (canAdd) {
                    if (!isFirstReplaced) {
                        UpdateItem(nextItem, index);
                        isFirstReplaced = true;
                    } else {
                        AddItem(nextItem);
                    }
                }
            }
        }

        protected virtual BuyPanelSideInfoData GenerateSideInfo(TItem item) {
            return null;
        }

        protected virtual bool IsBuyable(TItem item) {
            return true;
        }

        protected virtual void SortItems() {
            Func<TItem, int> sortMethod = (item) => item.Price;
            _data.OrderItems(sortMethod);
        }

        public virtual void LoadSave(GameData data) {
            _data.CheckDefaultItems(_defaultItemsToBuy);
        }
    }
}
