using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private int _maxSize = 100;
    [SerializeField] private Animator _animator;
    private List<IngredientCount> _ingredients = new List<IngredientCount>();
    private int _nowSize = 0;

    public List<IngredientCount> IngredientCounts => _ingredients;
    public int MaxSize => _maxSize;

    public event Action<IngredientCount> CountChanged;
    public event Action CarReturned;

    public int GetSpace()
    {
        return _maxSize - _nowSize;
    }

    public void Leave()
    {
        _ingredients.Clear();
        _animator.SetBool("isLeave", true);
    }

    public void Return()
    {
        _animator.SetBool("isLeave", false);
        CarReturned?.Invoke();
    }

    public void GetIngredients(int count, Ingredient plantedIngredient)
    {
        _nowSize += count;

        var index = GetIngredientIndex(plantedIngredient);
        if (index == -1) {
            var newElement = new IngredientCount {
                Count = count,
                Ingredient = plantedIngredient
            };
            _ingredients.Add(newElement);
            CountChanged?.Invoke(newElement);
        } else {
            _ingredients[index].Count += count;
            CountChanged?.Invoke(_ingredients[index]);
        }
    }

    private int GetIngredientIndex(Ingredient ingredient)
    {
        var haveIngredients = _ingredients.Select(x => x.Ingredient).ToList();
        return haveIngredients.IndexOf(ingredient);
    }
}
