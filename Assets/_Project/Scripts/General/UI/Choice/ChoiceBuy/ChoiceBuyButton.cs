using UnityEngine.UI;
public abstract class ChoiceBuyButton<TItem> : ChoiceButton<TItem>
    where TItem : BuyableItem {

    protected int _price;
    protected bool _isBuyable;

    protected override void Awake() {
        base.Awake();
        MoneyManager.Instance.MoneyChanged += CheckBuyable;
    }

    public virtual void Setup(TItem item, int index, ChoiceBuyUI<TItem> ui) {
        _button = GetComponent<Button>();
        gameObject.SetActive(true);

        Item = item;
        _price = item.Price;
        _icon.sprite = Item.Icon;
        CheckBuyable(MoneyManager.Instance.MoneyCount);
        _button.onClick.AddListener(
            delegate { ui.Choice(index, _isBuyable); }
        );
    }

    public virtual void CheckBuyable(int newValue) {
        _isBuyable = newValue >= _price;
    }
}
