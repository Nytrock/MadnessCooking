using System;
using UnityEngine;
using UnityEngine.UI;

public class IngredientBuyPanel : BaseInstantBuyPanel {
    [SerializeField] private Image _bedTypeImage;
    [SerializeField] private Material _grayscaleMaterial;
    private bool _isBedAvailable;

    public override Type Type => typeof(Ingredient);

    public override void Setup(BuyableObject item, BaseShop shop) {
        var ingredient = item as Ingredient;
        var ingredientShop = shop as IngredientShop;

        BedTypesManager bedTypesManager = ingredientShop.BedTypesManager;
        bedTypesManager.TypeAdded += UpdateBedAvailable;

        BedType bedType = bedTypesManager.GetBedWithIngredientType(ingredient.Type);
        if (bedType != null) {
            _bedTypeImage.sprite = bedType.Icon;
            _isBedAvailable = bedTypesManager.HaveBed(bedType);
            if (!_isBedAvailable)
                _bedTypeImage.material = _grayscaleMaterial;
        } else {
            _bedTypeImage.sprite = null;
            _isBedAvailable = true;
            _bedTypeImage.color = new Color(1, 1, 1, 0);
        }
        base.Setup(item, shop);
    }

    private void UpdateBedAvailable(BedType newBed) {
        if (_isBedAvailable)
            return;

        var ingredient = _item as Ingredient;
        _isBedAvailable = newBed.AcceptableType == ingredient.Type;
        UpdateButton(MoneyManager.Instance.MoneyCount);
    }

    protected override void UpdateButton(int moneyCount) {
        _buyButton.interactable = moneyCount >= _item.Price && _isBedAvailable;
    }
}
