using UnityEngine;

public class OrderRecipePart : FoodRecipePart
{
    [SerializeField] Color _haveColor;
    [SerializeField] Color _dontHaveColor;

    public override void Setup(IngredientCount count, bool isHave)
    {
        base.Setup(count, isHave);
        if (isHave)
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
