using UnityEngine;
using UnityEngine.UI;

public class BaseChooseShopItemView : MonoBehaviour
{
    [SerializeField] protected BaseChooseShop _shop;
    [SerializeField] private ItemInfoRendererWithNum _renderer;
    [SerializeField] private string _costText;
    [SerializeField] private Button _buyButton;
    protected BuyableObject _itemToBuy;

    private void Start()
    {
        ResetInfo();
        UpdateButton();
    }

    public void ShowItem(BuyableObject item)
    {
        if (item == _itemToBuy)
            ResetInfo();
        else
            SetInfo(item);
    }

    protected virtual void SetInfo(BuyableObject item)
    {
        _itemToBuy = item;
        _renderer.SetItemInfo(_itemToBuy);
        _renderer.SetNumText(_costText + _itemToBuy.Cost);
        UpdateButton();
    }

    public virtual void ResetInfo()
    {
        _itemToBuy = null;
        _renderer.ResetInfo();
        UpdateButton();
    }

    private void UpdateButton()
    {
        _buyButton.interactable = _itemToBuy != null;
    }

    public void BuyChosen()
    {
        _shop.BuyItem(_itemToBuy);
        ResetInfo();
    }
}
