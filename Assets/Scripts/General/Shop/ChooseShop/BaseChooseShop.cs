using UnityEngine;

public abstract class BaseChooseShop : BaseShop
{
    [SerializeField] private BaseChooseShopItemView _itemView;
    public BaseChooseBuyPanel NowPanel { get; private set; }

    public override void ChangeShopState(bool newState)
    {
        base.ChangeShopState(newState);

        if (!newState)
            _itemView.ResetInfo();
    }

    public override void BuyItem(BuyableObject item)
    {
        NowPanel.BuyChosenItem();
    }

    public void ChooseItem(BaseChooseBuyPanel panel)
    {
        NowPanel = panel;
        _itemView.ShowItem(panel.Item);
    }
}