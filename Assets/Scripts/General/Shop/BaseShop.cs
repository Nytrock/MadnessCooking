using UnityEngine;
using UnityEngine.Events;

public abstract class BaseShop : MonoBehaviour {
    [SerializeField] private GameObject _shop;
    [SerializeField] protected ShopCatalog _catalog;

    protected void LateStart() {
        GenerateShop();
        ChangeShopState(false);
    }

    public virtual void ChangeShopState(bool newState) {
        _shop.SetActive(newState);
        _catalog.ActivateFirstPage();
    }

    protected abstract void GenerateShop();
    public abstract void BuyItem(BuyableItem item);
    protected abstract UnityAction GetPanelAction(BuyableItem item);
}
