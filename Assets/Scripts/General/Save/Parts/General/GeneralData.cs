using System;

[Serializable]
public class GeneralData : ISaveable {
    public BuyableItemManagerData<Decor> DecorManager;

    public int MoneyCount;

    public int PopularityLevel;
    public int PopularityXp;

    public SerializableTimeSpan GlobalTime;

    public float Fatigue = 0;

    public int StartLocationIndex = 0;

    public float AutoSaveNowTime;

    public bool IsUpgradedTimeRenderer;
}
