using UnityEngine.UI;

public abstract class ChoiceBuyButton<T> : ChoiceButton<T> where T: BuyableObject
{
    private int _cost;
    protected bool _isBuyable;

    private void Start()
    {
        var moneyManager = MoneyManager.instance;
        moneyManager.MoneyChanged += CheckBuyable;
        CheckBuyable(moneyManager.MoneyAmount);
    }

    public virtual void Setup(T item, int index, ChoiceBuyUI<T> ui)
    {
        _button = GetComponent<Button>();
        gameObject.SetActive(true);

        Item = item;
        _cost = item.Cost;
        _icon.sprite = Item.Icon;
        _button.onClick.AddListener(
            delegate { ui.Choice(index, _isBuyable); }
        );
    }

    public virtual void CheckBuyable(int newValue)
    {
        _isBuyable = newValue >= _cost;
    }
}
