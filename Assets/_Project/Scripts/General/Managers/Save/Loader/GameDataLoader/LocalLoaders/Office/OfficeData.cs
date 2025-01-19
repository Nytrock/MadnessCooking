using System;

[Serializable]
public class OfficeData : ISaveable {
    public OfficeUpgradeData UpgradeData { get; set; }
    public SleepBedData SleepBed { get; set; }
    public ShopData<Decor> DecorShop { get; set; }
    public ShopData<Ingredient> IngredientShop { get; set; }
    public ShopData<Food> FoodShop { get; set; }
    public ShopData<Technic> TechnicShop { get; set; }
    public ShopData<BaseUpgrade> UpgradesShop { get; set; }
    public JokesData InternetJokesData { get; set; }
}
