using System;
using System.Collections.Generic;

[Serializable]
public class FarmData : ISaveable 
{
    public List<Ingredient> AvailableIngredients;
    public List<BedType> AvailableBedTypes;

    public SerializableSpaceManager FarmBedGroups = new();
    public SerializableFarmBed[] FarmBeds;

    public LimitedConsumableUpgradeHolder[] UpgradesHolders;
    public BaseUpgrade[] UpgradesToBuy;
    public List<BaseUpgrade> HaveUpgrades = new();

    public bool IsCowNextWheat = true;
    public bool IsWheatDistributing;

    public SerializableHoldAdd FarmWell = new();
    public SerializableNeedHoldAdd Cow = new();
    public SerializableNeedHoldAdd Puncher = new();
    public SerializableNeedHoldAdd FlourMill = new();
    public SerializableChickens Chickens = new();

    public SerializableIngredientStorage Car;
    public SerializableCarWaitManager CarWaitManager;

    public bool IsAutoWheat;
    public bool IsGrowStatusShow;

    public void GenerateFarmBeds(int lenght)
    {
        FarmBeds = new SerializableFarmBed[lenght];
        for (int i = 0; i < FarmBeds.Length; i++)
            FarmBeds[i] = new();
    }
}
