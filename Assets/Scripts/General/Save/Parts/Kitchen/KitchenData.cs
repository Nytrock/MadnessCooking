using System;

[Serializable]
public class KitchenData : ISaveable {
    public BuyableItemManagerData<Food> FoodManager;
    public IngredientStorageData KitchenStorage;

    public BuyableItemManagerData<Technic> TechnicManager;
    public TechnicHolderData[] TechnicHolders;

    public bool IsAutoSpice;
    public bool IsStrengthShow;
    public float TechnicCookSpeed = 1;
    public float TechnicRepairSpeed = 1;
    public float TechnicStrength = 1;
}
