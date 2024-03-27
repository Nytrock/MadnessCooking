using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(Food))]

public class Food : BuyableObject
{
    [SerializeField] private FoodType _type;
    [SerializeField] private Technic _typeTechnic;
    [SerializeField] private Sprite _miniSprite;
    [SerializeField, Min(0)] private float _timeToCook;
    [SerializeField, Min(0)] private float _timeToEat;
    [SerializeField, Min(0)] private int _moneyGet;
    [SerializeField] private IngredientCountList _ingredients;

    public FoodType Type => _type;
    public Technic TypeTechnic => _typeTechnic;
    public Sprite MiniSprite => _miniSprite;
    public float TimeToCook => _timeToCook;
    public float TimeToEat => _timeToEat;
    public int MoneyGet => _moneyGet;
    public IngredientCountList Ingredients => _ingredients;
}
