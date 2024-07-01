using System;

[Serializable]
public class GeneralData : ISaveable {
    public GeneralUpgradeData UpgradeData;
    public BuyableItemManagerData<BaseUpgrade> UpgradeManager;
    public BuyableItemManagerData<Decor> DecorManager;
    public MoneyManagerData MoneyManager;
    public PopularityManagerData PopularityManager;
    public TimeManagerData TimeManager;
    public FatigueManagerData FatigueManager;
    public LocationManagerData LocationManager;
    public AutoSaveManagerData AutoSaveManager;
}
