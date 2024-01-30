using System;
using UnityEngine;

public class Car : IngredientStorage
{
    [SerializeField] private Animator _animator;

    public event Action CarReturned;

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
}
