using System;

[Serializable]
public class FarmData : ISaveable {
    public FarmUpgradeData UpgradeData;
    public BuyableItemManagerData<Ingredient> IngredientManager;
    public BuyableItemManagerData<BedType> BedTypeManager;
    public WheatManagerData WheatManager;
    public FarmBedManagerData FarmBedGroups;
    public FarmShopData FarmShop;
    public HoldAddData FarmWell;
    public NeedHoldAddData Cow;
    public PuncherData Puncher;
    public NeedHoldAddData FlourMill;
    public ChickensData Chickens;
    public IngredientStorageData Car;
    public CarWaitManagerData CarWaitManager;
}
