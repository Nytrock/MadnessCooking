using MadnessCooking.General;
using UnityEngine;

namespace MadnessCooking.Office {
    public class UpgradeShop : BaseInstantShop<BaseUpgrade> {
        [SerializeField] private UpgradeTypeData[] _upgradeTypes;

        public override void LoadSave(GameData data) {
            data.Office.UpgradesShop ??= new(_defaultItemsToBuy);
            _data = data.Office.UpgradesShop;
        }

        protected override BuyPanelSideInfoData GenerateSideInfo(BaseUpgrade upgrade) {
            foreach (var type in _upgradeTypes)
                if (type.Type == upgrade.Type)
                    return new(type.Sprite, false, type.Name);
            return null;
        }
    }
}
