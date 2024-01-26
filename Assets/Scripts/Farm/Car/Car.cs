using System;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private int _maxSize = 100;
    [SerializeField] private Animator _animator;
    private readonly IngredientCountList _ingredients = new();
    private int _nowSize = 0;

    public int MaxSize => _maxSize;

    public event Action<int> CountChanged;
    public event Action CarReturned;

    public int GetSpace()
    {
        return _maxSize - _nowSize;
    }

    public void Leave()
    {
        _ingredients.Clear();
        _nowSize = 0;
        _animator.SetBool("isLeave", true);
    }

    public void Return()
    {
        _animator.SetBool("isLeave", false);
        CarReturned?.Invoke();
    }

    public void PutIngredients(int count, Ingredient plantedIngredient)
    {
        _nowSize += count;
        var newElement = new IngredientCount
        {
            Count = count,
            Ingredient = plantedIngredient
        };
        _ingredients.Add(newElement);

        var index = _ingredients.IndexOf(newElement);
        CountChanged?.Invoke(index);
    }

    public IngredientCount GetIngredient(int index)
    {
        return _ingredients.Get(index);
    }

    public IngredientCountList GetList()
    {
        return _ingredients.Copy();
    }
}
