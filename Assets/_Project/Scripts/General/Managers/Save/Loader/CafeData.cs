using System;
using MadnessCooking.Cafe;

namespace MadnessCooking.General {
    [Serializable]
    public class CafeData {
        public CafeUpgradeData UpgradeData { get; set; }
        public PopularityManagerData PopularityManager { get; set; }
        public SpaceManagerData SpaceManager { get; set; }
        public CafeNameManagerData CafeNameManager { get; set; }
        public CafeStateChangerData CafeOpener { get; set; }
        public ClientHolderManagerData ClientHolderManager { get; set; }
        public ClientsSpawnerData ClientsSpawner { get; set; }
        public CriticSpawnerData CriticSpawner { get; set; }
        public CameraManagerData CameraManager { get; set; }
    }
}
