using System;
using System.Collections.Generic;

[Serializable]
public class CafeData : ISaveable
{
    public SerializableSpaceManager Space = new();
    public bool IsOpened = true;
    public List<SerializableSpot> Spots = new();
    public List<SerializableClient> LeavingClients = new();
    public bool IsSpawning = true;
    public bool IsWaitingCritic;
    public bool IsEatTimeShow;
    public float NowSpawnTime;
    public float NeedSpawnTime;
}
