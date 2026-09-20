using UnityEngine;

namespace MadnessCooking.General {
    public abstract class LocalUpgradeManager : MonoBehaviour {
        public abstract void BindUpgradeData();
        public abstract void UpgradeAdded(BaseUpgrade upgrade);
    }
}
