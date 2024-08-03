using UnityEngine;
using UnityEngine.Events;

public abstract class BaseShop : MonoBehaviour {
    [SerializeField] protected ShopCatalog _catalog;

    protected virtual void LateStart() {
        ChangeShopState(false);
    }

    public virtual void ChangeShopState(bool newState) {
        if (newState) {
            GenerateShop();
            _catalog.ActivateFirstPage();
        }
    }

    protected abstract void GenerateShop();
    public abstract void BuyItem(BuyableItem item);
    protected abstract UnityAction GetPanelAction(BuyableItem item);
}
