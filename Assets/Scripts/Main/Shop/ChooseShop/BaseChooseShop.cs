using UnityEngine;

public abstract class BaseChooseShop : BaseShop
{
    [SerializeField] private BaseChooseShopItemView _itemView;
    protected BaseChooseBuyPanel _nowPanel;

    public override void ChangeShopState(bool newState)
    {
        base.ChangeShopState(newState);

        if (!newState)
            _itemView.ResetInfo();
    }

    public override void BuyItem(BuyableObject item)
    {
        _nowPanel.BuyChosenItem();
    }

    public void ChooseItem(BaseChooseBuyPanel panel)
    {
        _nowPanel = panel;
        _itemView.ShowItem(panel.Item);
    }
}