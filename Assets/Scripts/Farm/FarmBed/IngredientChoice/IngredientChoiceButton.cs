public class IngredientChoiceButton : ChoiceButton<Ingredient, IngredientChoiceUI>
{
    public override void Setup(Ingredient item, int index, IngredientChoiceUI ui)
    {
        base.Setup(item, index, ui);
        _icon.sprite = _item.Icon;
        _button.onClick.AddListener(
            delegate { ui.Choice(index); }
        );
    }
}
