using System;
using UnityEngine;

public abstract class SaveableBaseShop<TItem, TData> : BaseShop, IBindable<TData>
    where TItem : BuyableItem where TData : ISaveable {

    [SerializeField] protected TItem[] _defaultItemsToBuy;
    [SerializeField] protected BuyableItemManager<TItem> _itemManager;
    [SerializeField] protected ShopData<TItem> _data;

    protected override void GenerateShop() {
        SortItems();
        foreach (var item in _data.ItemsToBuy)
            _catalog.GeneratePanel(GeneratePanelData(item));
        _catalog.UpdateEmptyState();
    }

    protected virtual void UpdatePanels() {
        int index = 0;
        foreach (var item in _data.ItemsToBuy) {
            _catalog.UpdatePanel(index, GeneratePanelData(item));
            index++;
        }
    }

    public override void BuyItem(BuyableItem item) {
        BuyItem(item as TItem);
    }

    public virtual void BuyItem(TItem item) {
        MoneyManager.Instance.ChangeMoney(-item.Price);
        _data.BuyItem(item);
        _itemManager.AddItem(item);
        ChangePanelsState(item);
    }

    protected virtual void ChangePanelsState(TItem item) {
        int index = _data.IndexOfItemPanel(item);
        if ((item as IGraphable<TItem>) != null)
            CheckGraph(item, index);
        else
            RemoveItemPanel(item, index);
    }

    protected virtual void RemoveItemPanel(TItem item, int index) {
        _data.RemoveItemByIndex(index);
        _catalog.RemovePanel(index);
    }

    protected virtual void ReplaceItemPanel(TItem newItem, int index) {
        _data.ReplaceItemToBuy(newItem, index);
        _catalog.ReplacePanelItem(index, GeneratePanelData(newItem));
    }

    protected virtual void CheckGraph(TItem item, int index) {
        if (_data == null)
            throw new ArgumentNullException($"Data of shop {name} does not match the existing interface");

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
                    ReplaceItemPanel(nextItem, index);
                    isFirstReplaced = true;
                } else {
                    _data.AddItemToBuy(nextItem);
                    _catalog.GeneratePanel(GeneratePanelData(nextItem));
                }
            }
        }

        if (!isFirstReplaced)
            RemoveItemPanel(item, index);
    }

    private BuyPanelData GeneratePanelData(TItem item) {
        return new BuyPanelData(item, IsBuyable(item), GetPanelAction(item), GenerateSideInfo(item));
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
