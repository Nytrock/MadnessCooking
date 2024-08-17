using System;

[Serializable]
public class OfficeData : ISaveable {
    public OfficeUpgradeData UpgradeData;
    public OfficeBedData OfficeBed;
    public ShopData<Decor> DecorShop;
    public ShopData<Ingredient> IngredientShop;
    public ShopData<Food> FoodShop;
    public ShopData<Technic> TechnicShop;
    public ShopData<BaseUpgrade> UpgradesShop;
}
