using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class BaseChooseShopItemView : MonoBehaviour
{
    [SerializeField] protected BaseChooseShop _shop;
    [SerializeField] private ItemInfoRenderer _renderer;
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
        _renderer.SetCountText(_costText + _itemToBuy.Cost);
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
