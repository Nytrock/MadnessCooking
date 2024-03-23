using UnityEngine;

public class BedChoiceButton : ChoiceButton<BedType, BedChoiceUI>
{
    [SerializeField] private GameObject _blockedSprite;
    private int _cost;
    private bool _isBuyable;
    private bool _isBlocked;

    private void Start()
    {
        var moneyManager = MoneyManager.instance;
        moneyManager.MoneyChanged += CheckBuyable;
        CheckBuyable(moneyManager.MoneyAmount);
    }

    public override void Setup(BedType item, int index, BedChoiceUI ui)
    {
        base.Setup(item, index, ui);
        _icon.sprite = _item.Icon;
        _button.onClick.AddListener(
            delegate { ui.Choice(index, _isBuyable); }
        );
    }

    public void SetBlockedState(bool isHave)
    {
        _isBlocked = !isHave;
        _blockedSprite.SetActive(!isHave);
    }

    public void CheckBuyable(int newValue)
    {
        _isBuyable = newValue >= _cost && !_isBlocked;
    }
}