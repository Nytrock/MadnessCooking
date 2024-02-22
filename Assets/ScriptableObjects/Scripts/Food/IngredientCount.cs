using System;

[Serializable]
public class IngredientCount
{
    public IngredientCount(Ingredient ingredient, int count)
    {
        Ingredient = ingredient;
        Count = count;
    }

    public Ingredient Ingredient;
    public int Count;
}
