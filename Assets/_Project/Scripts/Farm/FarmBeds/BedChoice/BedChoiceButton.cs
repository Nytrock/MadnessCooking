using UnityEngine;

public class BedChoiceButton : ChoiceBuyButton<BedType> {
    [SerializeField] private GrayscaleImageRenderer _grayscaleImage;
    private bool _isBlocked;

    public void SetBlockedState(bool isBlocked) {
        _isBlocked = isBlocked;
        _grayscaleImage.SetGrayscaleVisibility(isBlocked);
    }

    public override void CheckBuyable(int newValue) {
        base.CheckBuyable(newValue);
        _isBuyable &= !_isBlocked;
    }
}