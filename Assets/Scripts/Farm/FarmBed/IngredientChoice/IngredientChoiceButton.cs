public class IngredientChoiceButton : ChoiceSimpleButton<Ingredient>
{
    public override void Setup(Ingredient item, int index, ChoiceSimpleUI<Ingredient> ui)
    {
        base.Setup(item, index, ui);
        _icon.sprite = _item.Icon;
        _button.onClick.AddListener(
            delegate { ui.Choice(index); }
        );
    }
}
