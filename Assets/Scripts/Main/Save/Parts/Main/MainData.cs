using System;

[Serializable]
public class MainData : ISaveable
{
    public int MoneyAmount;
    public int PopularityLevel;
    public int PopularityXp;
    public SerializableTimeSpan GlobalTime;
    public bool IsUpgradedTimeRenderer;
    public float Fatigue;
    public int LocationId;
}
