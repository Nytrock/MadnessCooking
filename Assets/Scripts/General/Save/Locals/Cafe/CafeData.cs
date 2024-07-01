using System;

[Serializable]
public class CafeData : ISaveable {
    public CafeUpgradeData UpgradeData;
    public SpaceManagerData Space;
    public CafeOpenerData CafeOpener;
    public CafeSpotManagerData SpotManager;
    public ClientsSpawnerData ClientsSpawner;
}
