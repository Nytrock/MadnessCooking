using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBuyPanel : MonoBehaviour
{
    [SerializeField] private ItemInfoRenderer _itemInfoRenderer;
    [SerializeField] private Button _buyButton;
    private BuyableObject _item;

    public void Setup(BuyableObject item)
    {
        _item = item;
        _itemInfoRenderer.SetItemInfo(item);

        UpdateButton(MoneyManager.instance.MoneyAmount);
        MoneyManager.instance.MoneyChanged += UpdateButton;
    }

    public void UpdateButton(int moneyCount)
    {
        _buyButton.interactable = moneyCount >= _item.Cost;
    }
}
