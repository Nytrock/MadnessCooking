using System;
using UnityEngine;

public abstract class SaveableBaseShop<TItem, TData> : BaseShop, IBindable<TData>
    where TItem : BuyableObject where TData : ISaveable {

    [SerializeField] protected TItem[] _defaultItemsToBuy;

    protected ShopData<TItem> _data;

    protected override void GenerateShop() {
        foreach (var item in _data.ItemsToBuy)
            _catalog.GeneratePanel(GeneratePanelData(item));
        _catalog.ActivateFirstPage();
    }

    protected virtual void UpdatePanels() {
        int index = 0;
        foreach (var item in _data.ItemsToBuy) {
            _catalog.UpdatePanel(index, GeneratePanelData(item));
            index++;
        }
    }

    public override void BuyItem(BuyableObject item) {
        BuyItem(item as TItem);
    }

    public virtual void BuyItem(TItem item) {
        MoneyManager.Instance.ChangeMoney(-item.Price);
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
        _data.RemoveItemToBuy(index);
        _catalog.RemovePanel(index);
    }

    protected virtual void ReplaceItemPanel(TItem newItem, int index) {
        _data.ReplaceItemToBuy(newItem, index);
        _catalog.ReplacePanelItem(index, GeneratePanelData(newItem));
    }

    protected virtual void CheckGraph(TItem item, int index) {
        GraphShopData<TItem> graphData = _data as GraphShopData<TItem>;
        if (graphData == null)
            throw new ArgumentNullException($"Data of shop {name} does not match the existing interface");
        graphData.BuyItem(item);

        bool isFirstReplaced = false;
        CheckNextItems(graphData, item, index, ref isFirstReplaced);
    }

    protected void CheckNextItems(GraphShopData<TItem> data, TItem item, int index, ref bool isFirstReplaced) {
        IGraphable<TItem> graphItem = item as IGraphable<TItem>;
        if (graphItem == null)
            return;

        foreach (var nextItem in graphItem.NextItems) {
            if (data.IsItemBuyable(nextItem))
                continue;

            if (data.IsItemAvailable(nextItem)) {
                CheckNextItems(data, nextItem, index, ref isFirstReplaced);
                continue;
            }

            bool canAdd = true;
            IGraphable<TItem> graphNextItem = nextItem as IGraphable<TItem>;
            if (graphNextItem != null)
                foreach (var needUpgrade in graphNextItem.NeedItems)
                    canAdd &= data.IsItemAvailable(needUpgrade);

            if (canAdd) {
                if (!isFirstReplaced) {
                    ReplaceItemPanel(nextItem, index);
                    isFirstReplaced = true;
                } else {
                    data.AddItemToBuy(nextItem);
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

    public virtual void Bind(TData data, bool isFileEmpty) {
        LateStart();
    }
}
