using UnityEngine.UI;

public class IngredientChoiceButton : ChoiceButton
{
    private Ingredient _ingredient;

    public void Setup(Ingredient newIngredient, int ingredientIndex, ChoiceUI ui)
    {
        _ingredient = newIngredient;
        gameObject.SetActive(true);
        _icon.sprite = _ingredient.Icon;
        GetComponent<Button>().onClick.AddListener(
            delegate { ui.Choice(ingredientIndex); }
            );
    }
}
