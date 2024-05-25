using UnityEngine;

public class OrderRecipePart : ShopFoodRecipePart
{
    [SerializeField] private Sprite _moneySprite;
    [SerializeField] private Color _availableColor;
    [SerializeField] private Color _notAvailableColor;

    public override void Setup(IngredientCount count, bool isAvailable)
    {
        base.Setup(count, isAvailable);
        if (isAvailable)
            _countText.color = _availableColor;
        else 
            _countText.color = _notAvailableColor;
    }

    public void SetupAutoSpice(IngredientCount ingredientCount)
    {
        gameObject.SetActive(true);
        _icon.sprite = _moneySprite;
        int cost = ingredientCount.Count * IngredientsManager.Instance.Spice.Cost;

        _countText.text = cost.ToString() + "x";
        if (MoneyManager.Instance.MoneyCount >= cost)
            _countText.color = _availableColor;
        else
            _countText.color = _notAvailableColor;
    }

    public override void Setup(Technic technic, bool isFree)
    {
        base.Setup(technic, isFree);
        if (isFree)
            _countText.color = _availableColor;
        else
            _countText.color = _notAvailableColor;
    }
}
