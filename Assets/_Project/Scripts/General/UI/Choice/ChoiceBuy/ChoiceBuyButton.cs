using UnityEngine.Events;

public abstract class ChoiceBuyButton<TItem> : ChoiceButton<TItem>
    where TItem : BuyableItem {

    protected int _price;
    protected bool _isBuyable;

    public bool IsBuyable => _isBuyable;

    protected override void Awake() {
        base.Awake();
        MoneyManager.Instance.MoneyChanged += CheckBuyable;
    }

    public override void Setup(TItem item, UnityAction buttonEvent) {
        base.Setup(item, buttonEvent);

        _price = item.Price;
        _icon.sprite = Item.Icon;
        CheckBuyable(MoneyManager.Instance.MoneyCount);
    }

    public virtual void CheckBuyable(int newMoneyCount) {
        _isBuyable = newMoneyCount >= _price;
    }
}
