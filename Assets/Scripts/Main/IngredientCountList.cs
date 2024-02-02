using System.Collections.Generic;
using System.Linq;

public class IngredientCountList
{
    private List<IngredientCount> _ingredientCounts = new();
    private List<Ingredient> _haveIngredients = new();

    public int Size => _ingredientCounts.Count;

    public void Add(IngredientCount ingredientCount)
    {
        if (ContainsIngredient(ingredientCount)) {
            var index = IndexOf(ingredientCount);
            _ingredientCounts[index].Count += ingredientCount.Count;
        } else {
            _ingredientCounts.Add(ingredientCount);
        }
        UpdateHaveIngredients();
    }

    public void Remove(IngredientCount ingredientCount)
    {
        if (!ContainsIngredient(ingredientCount))
            return;

        var index = IndexOf(ingredientCount);
        _ingredientCounts[index].Count -= ingredientCount.Count;
        if (_ingredientCounts[index].Count <= 0)
            _ingredientCounts.RemoveAt(index);

        UpdateHaveIngredients();
    }

    private void UpdateHaveIngredients()
    {
        _haveIngredients = _ingredientCounts.Select(x => x.Ingredient).ToList();
    }

    public bool ContainsIngredient(IngredientCount ingredientCount)
    {
        return _haveIngredients.Contains(ingredientCount.Ingredient);
    }

    public bool ContainsCount(IngredientCount ingredientCount)
    {
        if (!ContainsIngredient(ingredientCount))
            return false;
        return _ingredientCounts[IndexOf(ingredientCount)].Count >= ingredientCount.Count;
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
        _haveIngredients.Clear();
    }

    public IngredientCountList Copy()
    {
        var result = new IngredientCountList();
        foreach (var count in  _ingredientCounts)
            result.Add(count);
        return result;
    }
}
