using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private List<Food> _availableFood;

    public Food GetRandomFood()
    {
        return _availableFood[Random.Range(0, _availableFood.Count)];
    }
}
