using System;

[Serializable]
public class KitchenData : ISaveable {
    public KitchenUpgradeData UpgradeData;
    public BuyableItemManagerData<Food> FoodManager;
    public IngredientStorageData KitchenStorage;
    public BuyableItemManagerData<Technic> TechnicManager;

    public TechnicHolderData[] TechnicHolders;
}
