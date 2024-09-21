using System;

[Serializable]
public class KitchenData : ISaveable {
    public KitchenUpgradeData UpgradeData { get; set; }
    public BuyableItemManagerData<Food> FoodManager { get; set; }
    public IngredientStorageData KitchenStorage { get; set; }
    public BuyableItemManagerData<Technic> TechnicManager { get; set; }
    public TechnicHolderData[] TechnicHolders { get; set; }
    public KitchenCatData Cat { get; set; }
}
