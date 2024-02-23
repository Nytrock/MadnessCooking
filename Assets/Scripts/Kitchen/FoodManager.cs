using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private List<Food> _availableFood = new();

    public int FoodCount => _availableFood.Count;

    public Food GetRandomFood()
    {
        return _availableFood[Random.Range(0, _availableFood.Count)];
    }

    public void AddFood(Food newFood)
    {
        _availableFood.Add(newFood);
    }
}
