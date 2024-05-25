using System;
using System.Collections.Generic;

[Serializable]
public class CafeData : ISaveable
{
    public SpaceManagerData Space = new();
    public bool IsOpened = true;
    public List<SpotData> Spots = new();
    public List<ClientData> LeavingClients = new();
    public bool IsSpawning = true;
    public bool IsWaitingCritic;
    public bool IsEatTimeShow;
    public float NowSpawnTime;
    public float NeedSpawnTime;
}
