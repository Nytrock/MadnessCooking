using UnityEngine;
using UnityEngine.Events;

public abstract class BaseShop : MonoBehaviour {
    [SerializeField] protected ShopRenderer _renderer;

    public virtual void ChangeShopState(bool newState) {
        _renderer.ChangeShopState(newState);
    }

    public abstract void TryToBuyItem(BuyableItem item);
    protected abstract UnityAction GetPanelAction(BuyableItem item);
}
