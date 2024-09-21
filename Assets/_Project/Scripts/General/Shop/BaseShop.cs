using UnityEngine;
using UnityEngine.Events;

public abstract class BaseShop : MonoBehaviour {
    [SerializeField] protected ShopRenderer _renderer;

    protected virtual void LateStart() {
        ChangeShopState(false);
    }

    public virtual void ChangeShopState(bool newState) {
        _renderer.ChangeShopState(newState);
    }


    public abstract void BuyTutorialItems();
    protected abstract UnityAction GetPanelAction(BuyableItem item);
}
