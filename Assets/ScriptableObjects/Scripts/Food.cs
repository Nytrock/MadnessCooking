using UnityEngine;

[CreateAssetMenu(menuName = "Food")]

public class Food : BuyableObject
{
    public Technic TypeTechnic;
    public Sprite FoodSprite;
    public Sprite MiniSprite;
    public float TimeToCook;
    public float TimeToEat;
    public int MoneyGet;
    public IngredientCount[] Ingredients;
}
