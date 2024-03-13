using UnityEngine;

public class OrderRecipePart : ShopFoodRecipePart
{
    [SerializeField] private Sprite _moneySprite;
    [SerializeField] private Color _haveColor;
    [SerializeField] private Color _dontHaveColor;

    public override void Setup(IngredientCount count, bool isHave)
    {
        base.Setup(count, isHave);
        if (isHave)
            _count.color = _haveColor;
        else 
            _count.color = _dontHaveColor;
    }

    public void SetupAutoSpice(Ingredient spice, int count)
    {
        gameObject.SetActive(true);
        _icon.sprite = _moneySprite;
        _count.text = (spice.Cost * count).ToString() + "x";
        if (MoneyManager.instance.MoneyAmount >= spice.Cost * count)
            _count.color = _haveColor;
        else
            _count.color = _dontHaveColor;
    }

    public override void Setup(Technic technic, bool isFree)
    {
        base.Setup(technic, isFree);
        if (isFree)
            _count.color = _haveColor;
        else
            _count.color = _dontHaveColor;
    }
}
