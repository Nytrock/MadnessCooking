using UnityEngine;

public class BedChoiceButton : ChoiceBuyButton<BedType>
{
    [SerializeField] private GameObject _blockedSprite;
    private bool _isBlocked;

    public void SetBlockedState(bool isHave)
    {
        _isBlocked = !isHave;
        _blockedSprite.SetActive(!isHave);
    }

    public override void CheckBuyable(int newValue)
    {
        base.CheckBuyable(newValue);
        _isBuyable &= !_isBlocked;
    }
}