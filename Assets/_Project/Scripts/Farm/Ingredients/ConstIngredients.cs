using UnityEngine;

public class ConstIngredients : Singleton<ConstIngredients> {
    [SerializeField] private Ingredient _lemon;
    [SerializeField] private Ingredient _spice;
    [SerializeField] private Ingredient _milk;
    [SerializeField] private Ingredient _flour;
    [SerializeField] private Ingredient _egg;
    [SerializeField] private Ingredient _wheat;
    [SerializeField] private Ingredient _ectoplasm;
    [SerializeField] private string _moneyName = "Money";
    [SerializeField] private string _waterName = "Water";
    private Ingredient _money;
    private Ingredient _water;

    public Ingredient Lemon => _lemon;
    public Ingredient Spice => _spice;
    public Ingredient Milk => _milk;
    public Ingredient Flour => _flour;
    public Ingredient Egg => _egg;
    public Ingredient Wheat => _wheat;
    public Ingredient Ectoplasm => _ectoplasm;
    public Ingredient Money => _money;
    public Ingredient Water => _water;

    protected override void Awake() {
        base.Awake();
        _money = BuyableItem.CreateTemporaryItem<Ingredient>(_moneyName);
        _water = BuyableItem.CreateTemporaryItem<Ingredient>(_waterName);
    }
}
