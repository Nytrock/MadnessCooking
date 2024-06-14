using System;

[Serializable]
public class OfficeData : ISaveable {
    public DecorManagerData DecorData = new();
    public bool IsSleeping;
    public float InternetDownloadSpeed = 1;
    public bool IsInternetDownloadInstant;

    public GraphShopData<Decor> DecorShop;
    public ShopData<Ingredient> IngredientShop;
    public ShopData<Food> FoodShop;
    public ShopData<Technic> TechnicShop;
    public GraphShopData<BaseUpgrade> UpgradesShop;
}
