using System;
using System.Collections.Generic;

[Serializable]
public class PestsGeneratorData
{
    public List<PestData> Pests = new();
    public float NowTime;
    public float NeedTime;
    public bool IsActive;
    public bool IsPestsInstant;
    public bool IsPestsRemoved;
}
