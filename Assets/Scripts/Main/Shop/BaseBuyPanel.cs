using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBuyPanel : MonoBehaviour
{
    [SerializeField] protected Button _buyButton;
    protected BuyableObject _item;
    public abstract Type Type { get; }

    public virtual void Setup(BuyableObject item, BaseShop shop)
    {
        _item = item;
        SetVisual(item);
        SetButtonListener(shop);
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    protected abstract void SetButtonListener(BaseShop shop);
    public abstract void SetVisual(BuyableObject item);
}
