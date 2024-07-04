using System;

[Serializable]
public class KitchenData : ISaveable {
    public KitchenUpgradeData UpgradeData;
    public BuyableItemContainerData<Food> FoodManager;
    public IngredientStorageData KitchenStorage;
    public BuyableItemContainerData<Technic> TechnicManager;
    public TechnicHolderData[] TechnicHolders;
}
