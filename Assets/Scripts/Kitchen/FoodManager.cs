using System.Linq;
using UnityEngine;

public class FoodManager : MonoBehaviour, IBindable<KitchenData>
{
    [SerializeField] Food[] _defaultFood;
    private KitchenData _data;

    public int FoodCount => _data.AvailableFood.Count;

    public Food GetRandomFood()
    {
        return _data.AvailableFood[Random.Range(0, FoodCount)];
    }

    public void AddFood(Food newFood)
    {
        _data.AvailableFood.Add(newFood);
    }

    public void Bind(KitchenData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty)
            _data.AvailableFood = _defaultFood.ToList();
    }
}
