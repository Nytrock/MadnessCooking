using System;
using System.Collections.Generic;

[Serializable]
public class FarmData : ISaveable {
    public List<Ingredient> AvailableIngredients;
    public List<BedType> AvailableBedTypes;

    public SpaceManagerData FarmBedGroups = new();
    public FarmBedData[] FarmBeds;

    public LimitedConsumableUpgradeHolder[] UpgradesHolders;
    public BaseUpgrade[] UpgradesToBuy;
    public List<BaseUpgrade> AvailableUpgrades = new();

    public bool IsCowNextWheat = true;
    public bool IsWheatDistributing;

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

    public void GenerateFarmBeds(int lenght) {
        FarmBeds = new FarmBedData[lenght];
        for (int i = 0; i < FarmBeds.Length; i++)
            FarmBeds[i] = new();
    }
}
