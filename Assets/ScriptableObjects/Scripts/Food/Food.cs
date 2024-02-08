using UnityEngine;

[CreateAssetMenu(menuName = "Food")]

public class Food : BuyableObject
{
    public FoodType Type;
    public Technic TypeTechnic;
    public Sprite MiniSprite;
    public float TimeToCook;
    public float TimeToEat;
    public int MoneyGet;
    public IngredientCount[] Ingredients;
}
