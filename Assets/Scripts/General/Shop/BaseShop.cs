using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseShop : MonoBehaviour {
    public abstract int ItemsCount { get; }

    public event Action ShopLoaded;
    public event Action<bool> ShopStateChanged;


    public event Action ItemAdded;
    public event Action<int> ItemRemoved;
    public event Action<int, BuyableItem> ItemUpdated;

    protected virtual void LateStart() {
        ChangeShopState(false);
        ShopLoaded?.Invoke();
    }

    protected void InvokeItemUpdated(int index, BuyableItem item) {
        ItemUpdated?.Invoke(index, item);
    }

    protected void InvokeItemAdded() {
        ItemAdded?.Invoke();
    }

    protected void InvokeItemRemoved(int index) {
        ItemRemoved?.Invoke(index);
    }

    public virtual void ChangeShopState(bool newState) {
        ShopStateChanged?.Invoke(newState);
    }


    public abstract BuyPanelData GetPanelData(int index);
    public abstract void BuyTutorialItems();
    protected abstract UnityAction GetPanelAction(BuyableItem item);
}
