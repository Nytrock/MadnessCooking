using System;

[Serializable]
public class FarmData : ISaveable {
    public FarmUpgradeData UpgradeData;
    public BuyableItemContainerData<Ingredient> IngredientManager;
    public BuyableItemContainerData<BedType> BedTypeManager;
    public WheatManagerData WheatManager;
    public FarmBedManagerData FarmBedGroups;
    public FarmShopData FarmShop;
    public HoldAddData FarmWell;
    public NeedHoldAddData Cow;
    public ShitGeneratorData ShitGenerator;
    public NeedHoldAddData Puncher;
    public NeedHoldAddData FlourMill;
    public ChickensData Chickens;
    public IngredientStorageData Car;
    public CarWaitManagerData CarWaitManager;
}
