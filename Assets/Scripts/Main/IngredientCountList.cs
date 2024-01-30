using System.Collections.Generic;
using System.Linq;

public class IngredientCountList
{
    private List<IngredientCount> _ingredientCounts = new();
    private List<Ingredient> _haveIngredients = new();

    public int Size => _ingredientCounts.Count;

    public void Add(IngredientCount ingredientCount)
    {
        if (Contains(ingredientCount)) {
            var index = IndexOf(ingredientCount);
            _ingredientCounts[index].Count += ingredientCount.Count;
        } else {
            _ingredientCounts.Add(ingredientCount);
        }
        UpdateHaveIngredients();
    }

    private void UpdateHaveIngredients()
    {
        _haveIngredients = _ingredientCounts.Select(x => x.Ingredient).ToList();
    }

    public bool Contains(IngredientCount ingredientCount)
    {
        return _haveIngredients.Contains(ingredientCount.Ingredient);
    }

    public int IndexOf(IngredientCount ingredientCount)
    {
        return _haveIngredients.IndexOf(ingredientCount.Ingredient);
    }

    public IngredientCount Get(int index)
    {
        return _ingredientCounts[index];
    }

    public void Clear()
    {
        _ingredientCounts.Clear();
    }

    public IngredientCountList Copy()
    {
        var result = new IngredientCountList();
        foreach (var count in  _ingredientCounts)
            result.Add(count);
        return result;
    }
}
