using System;
using UnityEngine;

public abstract class BaseShop : MonoBehaviour
{
    [SerializeField] private GameObject _shop;
    [SerializeField] protected ShopCatalog _catalog;
    public abstract Type Type { get; }

    protected BuyableObject[] _itemsToBuy;

    private void Start()
    {
        GenerateShop();
        ChangeShopState(false);
    }

    public virtual void ChangeShopState(bool newState)
    {
        _shop.SetActive(newState);
        _catalog.ActivateFirstPage();
    }

    protected void GenerateShop() {
        SetObjectsArray();
        _catalog.SetShop(this);
        for (int i = 0; i < _itemsToBuy.Length; i++)
            _catalog.GeneratePanel(_itemsToBuy[i]);
        _catalog.ActivateFirstPage();
    }

    protected abstract void SetObjectsArray();
    public abstract void BuyItem(BuyableObject item);
}
