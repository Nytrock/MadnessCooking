using System;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class IngredientBuyPanel : BaseInstantBuyPanel
{
    [SerializeField] private Image _bedTypeImage;
    [SerializeField] private Material _grayscaleMaterial;
    private bool _isHaveBed;

    public override Type Type => typeof(Ingredient);

    public override void Setup(BuyableObject item, BaseShop shop)
    {
        var ingredient = item as Ingredient;
        var ingredientShop = shop as IngredientShop;

        var bedTypesManager = ingredientShop.BedTypesManager;
        bedTypesManager.TypeAdded += UpdateHaveBed;

        var bedType = bedTypesManager.GetBedWithIngredientType(ingredient.Type);
        if (bedType != null) {
            _bedTypeImage.sprite = bedType.Icon;
            _isHaveBed = bedTypesManager.HaveBed(bedType);
            if (!_isHaveBed)
                _bedTypeImage.material = _grayscaleMaterial;
        } else {
            _bedTypeImage.sprite = null;
            _isHaveBed = true;
            _bedTypeImage.color = new Color(1, 1, 1, 0);
        }
        base.Setup(item, shop);
    }

    private void UpdateHaveBed(BedType newBed)
    {
        if (_isHaveBed)
            return;

        var ingredient = _item as Ingredient;
        _isHaveBed = newBed.AcceptableType == ingredient.Type;
        UpdateButton(MoneyManager.instance.MoneyAmount);
    }

    protected override void UpdateButton(int moneyCount)
    {
        _buyButton.interactable = moneyCount >= _item.Cost && _isHaveBed;
    }
}
