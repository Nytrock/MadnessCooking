using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(Food))]

public class Food : BuyableObject
{
    [SerializeField] private FoodType _type;
    [SerializeField] private Technic _typeTechnic;
    [SerializeField] private Sprite _miniSprite;
    [SerializeField] private float _timeToCook;
    [SerializeField] private float _timeToEat;
    [SerializeField] private int _moneyGet;
    [SerializeField] private IngredientCountList _ingredients;

    public FoodType Type => _type;
    public Technic TypeTechnic => _typeTechnic;
    public Sprite MiniSprite => _miniSprite;
    public float TimeToCook => _timeToCook;
    public float TimeToEat => _timeToEat;
    public int MoneyGet => _moneyGet;
    public IngredientCountList Ingredients => _ingredients;
}
