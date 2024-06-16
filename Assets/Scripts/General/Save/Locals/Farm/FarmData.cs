using System;

[Serializable]
public class FarmData : ISaveable {
    public FarmUpgradeData UpgradeData;
    public BuyableItemManagerData<Ingredient> IngredientManager;
    public BuyableItemManagerData<BedType> BedTypeManager;
    public WheatManagerData WheatManager;
    public SpaceManagerData FarmBedGroups;

    public FarmBedData[] FarmBeds;

    public FarmShopData FarmShop;
    public HoldAddData FarmWell = new();
    public NeedHoldAddData Cow;
    public ShitGeneratorData ShitGenerator;
    public NeedHoldAddData Puncher = new();
    public NeedHoldAddData FlourMill;
    public ChickensData Chickens;
    public IngredientStorageData Car;
    public CarWaitManagerData CarWaitManager;

    public void GenerateFarmBeds(int lenght) {
        FarmBeds = new FarmBedData[lenght];
        for (int i = 0; i < FarmBeds.Length; i++)
            FarmBeds[i] = new();
    }
}
