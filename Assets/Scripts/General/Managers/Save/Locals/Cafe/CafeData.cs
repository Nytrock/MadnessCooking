using System;

[Serializable]
public class CafeData : ISaveable {
    public CafeUpgradeData UpgradeData;
    public SpaceManagerData Space;
    public CafeStateChangerData CafeOpener;
    public CafeSpotManagerData SpotManager;
    public ClientsSpawnerData ClientsSpawner;
    public CriticSpawnerData CriticSpawner;
}
