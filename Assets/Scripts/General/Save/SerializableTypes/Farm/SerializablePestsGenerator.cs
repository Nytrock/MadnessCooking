using System;
using System.Collections.Generic;

[Serializable]
public class SerializablePestsGenerator
{
    public List<SerializablePest> Pests = new();
    public float NowTime;
    public float NeedTime;
    public bool IsActive;
    public bool IsPestsInstant;
    public bool IsPestsRemoved;
}
