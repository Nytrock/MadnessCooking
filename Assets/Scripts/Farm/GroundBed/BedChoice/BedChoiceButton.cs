using UnityEngine.UI;

public class BedChoiceButton : ChoiceButton
{
    private BedType _type;
    private int _cost;
    private bool _isBuyable;

    private void Start()
    {
        var moneyManager = MoneyManager.instance;
        moneyManager.MoneyChanged += CheckBuyable;
        CheckBuyable(moneyManager.MoneyAmount);
    }

    public void Setup(BedType newType, int typeIndex, BedChoiceUI ui)
    {
        gameObject.SetActive(true);
        _type = newType;
        _cost = _type.Cost;
        _icon.sprite = _type.BedSprite;
        GetComponent<Button>().onClick.AddListener(
            delegate { ui.Choice(typeIndex, _isBuyable); }
            );
    }

    public void CheckBuyable(int newValue)
    {
        _isBuyable = newValue >= _cost;
    }
}