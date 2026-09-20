using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class FarmBedUpgradeManager : MonoBehaviour {
        [SerializeField] private UpgradeManager _upgradeManager;
        private List<FarmBedUpgrade> _availableUpgrades;

        private void Awake() {
            _availableUpgrades = new();
            _upgradeManager.ItemAdded += CheckAddedUpgrade;
        }

        public IEnumerable<FarmBedUpgrade> GetAvailableUpgrades() {
            foreach (var upgrade in _availableUpgrades)
                yield return upgrade;
        }

        public void CheckAddedUpgrade(BaseUpgrade upgrade) {
            if (upgrade as FarmBedUpgrade != null) {
                _availableUpgrades.Add(upgrade as FarmBedUpgrade);
                _availableUpgrades = _availableUpgrades.OrderBy(upgrade => upgrade.Price).ToList();
            }
        }

        public bool HaveUpgrade(FarmBedUpgrade upgrade) {
            return _availableUpgrades.Contains(upgrade);
        }
    }
}
