using System;
using System.Collections.Generic;

[Serializable]
public class CafeData : ISaveable {
    public CafeUpgradeData UpgradeData;
    public SpaceManagerData Space;

    public bool IsOpened = true;

    public List<SpotData> Spots = new();

    public List<ClientData> LeavingClients = new();
    public bool IsSpawning = true;
    public float NowSpawnTime;
    public float NeedSpawnTime;

    public bool IsWaitingCritic;
}
