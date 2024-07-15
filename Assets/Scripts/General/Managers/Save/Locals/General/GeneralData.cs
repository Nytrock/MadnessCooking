using System;

[Serializable]
public class GeneralData : ISaveable {
    public GeneralUpgradeData UpgradeData;
    public BuyableItemContainerData<BaseUpgrade> UpgradeManager;
    public BuyableItemContainerData<Decor> DecorManager;
    public MoneyManagerData MoneyManager;
    public PopularityManagerData PopularityManager;
    public TimeManagerData TimeManager;
    public LightManagerData LightManager;
    public FatigueManagerData FatigueManager;
    public LocationManagerData LocationManager;
}
