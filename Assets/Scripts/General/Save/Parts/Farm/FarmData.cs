using System;

[Serializable]
public class FarmData : ISaveable {
    public BuyableItemManagerData<Ingredient> IngredientManager;
    public BuyableItemManagerData<BedType> BedTypeManager;

    public SpaceManagerData FarmBedGroups = new();
    public FarmBedData[] FarmBeds;

    public FarmShopData FarmShop;


    public HoldAddData FarmWell = new();
    public NeedHoldAddData Cow = new();
    public ShitGeneratorData ShitGenerator = new();
    public NeedHoldAddData Puncher = new();
    public NeedHoldAddData FlourMill = new();
    public ChickensData Chickens = new();

    public IngredientStorageData Car;

    public CarWaitManagerData CarWaitManager;

    public bool IsAutoWheat;
    public bool IsGrowStatusShow;
    public bool IsCowNextWheat = true;
    public bool IsWheatDistributing;

    public void GenerateFarmBeds(int lenght) {
        FarmBeds = new FarmBedData[lenght];
        for (int i = 0; i < FarmBeds.Length; i++)
            FarmBeds[i] = new();
    }
}
