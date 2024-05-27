using UnityEngine;

public class ConstIngredients : Singleton<ConstIngredients>
{
    [SerializeField] private Ingredient _lemon;
    [SerializeField] private Ingredient _spice;
    [SerializeField] private Ingredient _milk;
    [SerializeField] private Ingredient _flour;
    [SerializeField] private Ingredient _egg;
    [SerializeField] private Ingredient _wheat;

    public Ingredient Lemon => _lemon;
    public Ingredient Spice => _spice;
    public Ingredient Milk => _milk;
    public Ingredient Flour => _flour;
    public Ingredient Egg => _egg;
    public Ingredient Wheat => _wheat;
}
