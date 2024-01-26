using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngredientCountList
{
    private List<IngredientCount> _ingredientCounts = new();

    public void Add(IngredientCount ingredientCount)
    {
        if (Contains(ingredientCount)) {
            var index = IndexOf(ingredientCount);
            _ingredientCounts[index].Count += ingredientCount.Count;
        } else {
            _ingredientCounts.Add(ingredientCount);
        }
    }

    public bool Contains(IngredientCount ingredientCount)
    {
        var haveIngredients = _ingredientCounts.Select(x => x.Ingredient).ToList();
        return haveIngredients.Contains(ingredientCount.Ingredient);
    }

    public int IndexOf(IngredientCount ingredientCount)
    {
        var haveIngredients = _ingredientCounts.Select(x => x.Ingredient).ToList();
        return haveIngredients.IndexOf(ingredientCount.Ingredient);
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
