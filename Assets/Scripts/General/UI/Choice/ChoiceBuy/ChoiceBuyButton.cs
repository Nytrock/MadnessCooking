using UnityEngine.UI;
public abstract class ChoiceBuyButton<TItem> : ChoiceButton<TItem> where TItem: BuyableObject
{
    private int _cost;
    protected bool _isBuyable;

    private void Awake()
    {
        MoneyManager.instance.MoneyChanged += CheckBuyable;
    }

    public virtual void Setup(TItem item, int index, ChoiceBuyUI<TItem> ui)
    {
        _button = GetComponent<Button>();
        gameObject.SetActive(true);

        Item = item;
        _cost = item.Cost;
        _icon.sprite = Item.Icon;
        CheckBuyable(MoneyManager.instance.MoneyCount);
        _button.onClick.AddListener(
            delegate { ui.Choice(index, _isBuyable); }
        );
    }

    public virtual void CheckBuyable(int newValue)
    {
        _isBuyable = newValue >= _cost;
    }
}
