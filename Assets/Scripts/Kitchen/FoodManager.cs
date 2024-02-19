using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private List<Food> _availableFood = new List<Food>();

    public int FoodCount => _availableFood.Count;

    public Food GetRandomFood()
    {
        return _availableFood[Random.Range(0, _availableFood.Count)];
    }
}
