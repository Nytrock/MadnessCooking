using UnityEngine;
using UnityEngine.Events;

public abstract class BaseShop : MonoBehaviour {
    [SerializeField] protected ShopCatalog _catalog;
    private bool _isShopGenerated;

    protected virtual void LateStart() {
        ChangeShopState(false);
    }

    public virtual void ChangeShopState(bool newState) {
        if (newState && !_isShopGenerated) {
            GenerateShop();
            _isShopGenerated = true;
        }

        if (newState)
            _catalog.ActivateFirstPage();
    }

    protected abstract void GenerateShop();
    public abstract void BuyItem(BuyableItem item);
    protected abstract UnityAction GetPanelAction(BuyableItem item);
}
