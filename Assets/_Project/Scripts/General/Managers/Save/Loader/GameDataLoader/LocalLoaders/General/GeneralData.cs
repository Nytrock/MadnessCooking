using System;

[Serializable]
public class GeneralData : ISaveable {
    public GeneralUpgradeData UpgradeData { get; set; }
    public BuyableItemManagerData<Decor> DecorManager { get; set; }
    public MoneyManagerData MoneyManager { get; set; }
    public GameTimeManagerData GameTimeManager { get; set; }
    public RealTimeManagerData RealTimeManager { get; set; }
    public LightManagerData LightManager { get; set; }
    public FatigueManagerData FatigueManager { get; set; }
    public LocationManagerData LocationManager { get; set; }
    public TutorialManagerData TutorialManager { get; set; }
    public GraymanData Grayman { get; set; }
}
