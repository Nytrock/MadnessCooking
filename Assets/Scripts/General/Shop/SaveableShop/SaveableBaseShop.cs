using System;
using System.Linq;
using UnityEngine;

public abstract class SaveableBaseShop<TItem, TData> : BaseShop, IBindable<TData>
    where TItem : BuyableItem where TData : ISaveable {

    [SerializeField] protected TItem[] _defaultItemsToBuy;
    [SerializeField] private TItem[] _tutorialItems;
    [SerializeField] protected BuyableItemManager<TItem> _itemManager;
    [SerializeField] protected ShopData<TItem> _data;

    public override int ItemsCount => _data.ItemsToBuy.Count();
    public event Action<BuyableItem> ItemBought;

    protected override void LateStart() {
        SortItems();
        base.LateStart();
    }

    public override BuyPanelData GetPanelData(int index) {
        TItem item = _data.GetItem(index);
        return new BuyPanelData(item, IsBuyable(item), GetPanelAction(item), GenerateSideInfo(item));
    }

    protected virtual void UpdatePanels() {
        int index = 0;
        foreach (var item in _data.ItemsToBuy) {
            InvokeItemUpdated(index, item);
            index++;
        }
    }

    public override void BuyTutorialItems() {
        foreach (var item in _tutorialItems)
            BuyItem(item);
    }

    public virtual void BuyItem(TItem item) {
        MoneyManager.Instance.ChangeMoney(-item.Price);
        _data.BuyItem(item);
        _itemManager.AddItem(item);
        UpdateItemsAfterBuying(item);
        ItemBought?.Invoke(item);
    }

    protected virtual void UpdateItemsAfterBuying(TItem buyedItem) {
        int index = _data.IndexOfItem(buyedItem);
        if ((buyedItem as IGraphable<TItem>) != null)
            CheckGraph(buyedItem, index);
        else
            RemoveItem(buyedItem, index);
    }

    protected virtual void RemoveItem(TItem item, int index) {
        _data.RemoveItemByIndex(index);
        InvokeItemRemoved(index);
    }

    protected virtual void UpdateItem(TItem newItem, int index) {
        _data.ReplaceItemToBuy(newItem, index);
        InvokeItemUpdated(index, newItem);
    }

    protected void AddItem(TItem newItem) {
        _data.AddItemToBuy(newItem);
        InvokeItemAdded();
    }

    protected virtual void CheckGraph(TItem item, int index) {
        bool isFirstReplaced = false;
        CheckNextItems(item, index, ref isFirstReplaced);
    }

    protected void CheckNextItems(TItem item, int index, ref bool isFirstReplaced) {
        IGraphable<TItem> graphItem = item as IGraphable<TItem>;
        if (graphItem == null)
            return;

        foreach (var nextItem in graphItem.NextItems) {
            if (_data.IsItemBuyable(nextItem))
                continue;

            if (_data.IsItemAvailable(nextItem)) {
                CheckNextItems(nextItem, index, ref isFirstReplaced);
                continue;
            }

            bool canAdd = true;
            IGraphable<TItem> graphNextItem = nextItem as IGraphable<TItem>;
            if (graphNextItem != null)
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

        if (!isFirstReplaced)
            RemoveItem(item, index);
    }

    protected virtual GrayscaleImageData GenerateSideInfo(TItem item) {
        return null;
    }

    protected virtual bool IsBuyable(TItem item) {
        return true;
    }

    protected virtual void SortItems() {
        Func<TItem, int> sortMethod = (item) => item.Price;
        _data.OrderItems(sortMethod);
    }

    public virtual void Bind(TData data) {
        _data.CheckDefaultItems(_defaultItemsToBuy);
        LateStart();
    }
}
