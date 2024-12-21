using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(Food))]

public class Food : BuyableItem {
    [SerializeField] private FoodType _type;
    [SerializeField] private Technic _typeTechnic;
    [SerializeField] private Sprite _miniSprite;
    [SerializeField] private Sprite _cookingSprite;
    [SerializeField, Min(0)] private float _timeToCook;
    [SerializeField, Min(0)] private float _timeToEat;
    [SerializeField, Min(0)] private int _moneyGet;
    [SerializeField] private BuyableItemCountList<Ingredient> _ingredients;
    [SerializeField] private Color _color;
    [SerializeField] private bool _isNeedWater;

    protected override string _table => nameof(Food) + "Table";

    public FoodType Type => _type;
    public Technic TypeTechnic => _typeTechnic;
    public Sprite MiniSprite => _miniSprite;
    public Sprite CookingSprite => _cookingSprite;
    public float TimeToCook => _timeToCook;
    public float TimeToEat => _timeToEat;
    public int MoneyGet => _moneyGet + SpicesPrice;
    public Color Color => _color;
    public bool IsNeedWater => _isNeedWater;

    public IEnumerable<BuyableItemCount<Ingredient>> Ingredients => _ingredients.GetItems();
    public int CountIngredients => _ingredients.Size;

    public int SpicesPrice {
        get {
            int price = 0;
            foreach (var ingredient in _ingredients) {
                if (ingredient.Item == ConstIngredients.Instance.Spice) {
                    price += ingredient.Count * ingredient.Item.Price;
                }
            }
            return price;
        }
    }
}
