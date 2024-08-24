using System;

[Serializable]
public class CafeData : ISaveable {
    public CafeUpgradeData UpgradeData { get; set; }
    public SpaceManagerData Space { get; set; }
    public CafeStateChangerData CafeOpener { get; set; }
    public CafeSpotManagerData SpotManager { get; set; }
    public ClientsSpawnerData ClientsSpawner { get; set; }
    public CriticSpawnerData CriticSpawner { get; set; }
}
