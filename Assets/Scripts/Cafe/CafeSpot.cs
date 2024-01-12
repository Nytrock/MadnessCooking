using UnityEngine;

public class CafeSpot : MonoBehaviour
{
    [SerializeField] private CafeSeat [] _seats;
    [SerializeField] private SpriteRenderer [] _tableFoods;
    private int _index;

    public int Index => _index;

    public int SeatsCount => _seats.Length;

    public Transform GetTarget(int index = 0) { return _seats[index].transform; }

    public void SetTableFoodSprite(Food food, int index)
    {
        _tableFoods[index].sprite = food.MiniSprite;
    }

    public void ResetTableFoodSprite(int index)
    {
        _tableFoods[index].sprite = null;
    }
}
