using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBuyPanel : MonoBehaviour
{
    [SerializeField] protected ItemInfoRenderer _itemInfoRenderer;
    [SerializeField] protected Button _buyButton;
    protected BuyableObject _item;

    public virtual void Setup(BuyableObject item, BaseShop shop)
    {
        _item = item;
        _itemInfoRenderer.SetItemInfo(item);
        SetButtonListener(shop);
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    protected abstract void SetButtonListener(BaseShop shop);
}
