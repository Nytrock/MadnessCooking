using System;

[Serializable]
public class OfficeData : ISaveable {
    public OfficeUpgradeData UpgradeData;
    public OfficeBedData OfficeBed;
    public GraphShopData<Decor> DecorShop;
    public ShopData<Ingredient> IngredientShop;
    public ShopData<Food> FoodShop;
    public ShopData<Technic> TechnicShop;
    public GraphShopData<BaseUpgrade> UpgradesShop;
}
