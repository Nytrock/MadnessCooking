using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBuyPanel : MonoBehaviour
{
    [SerializeField] private ItemInfoRenderer _itemInfoRenderer;
    [SerializeField] private string _costText;
    [SerializeField] private Button _buyButton;
    protected BuyableObject _item;

    public void Setup(BuyableObject item, BaseShop shop)
    {
        _item = item;
        _itemInfoRenderer.SetItemInfo(item);
        _itemInfoRenderer.SetCountText(_costText + item.Cost);
        _buyButton.onClick.AddListener(delegate { shop.BuyItem(item); BuyItem(); });

        UpdateButton(MoneyManager.instance.MoneyAmount);
        MoneyManager.instance.MoneyChanged += UpdateButton;
    }

    public void UpdateButton(int moneyCount)
    {
        _buyButton.interactable = moneyCount >= _item.Cost;
    }


    public void Destroy()
    {
        MoneyManager.instance.MoneyChanged -= UpdateButton;
        Destroy(gameObject);
    }

    protected abstract void BuyItem();
}
