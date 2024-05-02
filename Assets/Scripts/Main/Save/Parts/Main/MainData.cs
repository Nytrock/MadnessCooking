using System;

[Serializable]
public class MainData : ISaveable
{
    public int MoneyCount;
    public int PopularityLevel;
    public int PopularityXp;
    public SerializableTimeSpan GlobalTime;
    public bool IsUpgradedTimeRenderer;
    public float Fatigue = 0;
    public int StartLocationId = 0;
}
