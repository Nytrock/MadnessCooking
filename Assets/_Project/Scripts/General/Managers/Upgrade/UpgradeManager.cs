using System.Linq;
using UnityEngine;

namespace MadnessCooking.General {
    public class UpgradeManager : BuyableItemManager<BaseUpgrade> {
        [SerializeField, Interface(typeof(IUpgradeable))] private MonoBehaviour[] _upgradeables;

        public override void AddItem(BaseUpgrade upgrade) {
            base.AddItem(upgrade);
            foreach (var upgradeable in _upgradeables.Cast<IUpgradeable>())
                upgradeable.CheckAddedUpgrade(upgrade);
        }

        public override void LoadSave(GameData data) {
            data.UpgradeManager ??= new(_defaultItems);
            _data = data.UpgradeManager;

            BindUpgradeData(data);
        }

        private void BindUpgradeData(GameData data) {
            foreach (var upgradeable in _upgradeables.Cast<IUpgradeable>())
                upgradeable.SetUpgradeData(data);
        }
    }
}
