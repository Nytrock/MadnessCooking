using System;

[Serializable]
public class GeneralData : ISaveable {
    public GeneralUpgradeData UpgradeData;
    public BuyableItemManagerData<Decor> DecorManager;
    public MoneyManagerData MoneyManager;
    public PopularityManagerData PopularityManager;
    public GameTimeManagerData GameTimeManager;
    public RealTimeManagerData RealTimeManager;
    public LightManagerData LightManager;
    public FatigueManagerData FatigueManager;
    public LocationManagerData LocationManager;
}
