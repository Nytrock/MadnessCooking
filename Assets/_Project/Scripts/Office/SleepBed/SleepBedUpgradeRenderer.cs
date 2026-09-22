using MadnessCooking.General;

namespace MadnessCooking.Office {
    public class SleepBedUpgradeRenderer : UpgradeRenderer, IUpgradeable {
        private OfficeUpgradeData _upgradeData;

        public void SetUpgradeData(GameData gameData) {
            _upgradeData = gameData.Office.UpgradeData;
        }

        protected override void ChangeState(bool newState) {
            base.ChangeState(newState);
            if (newState)
                _upgradeData.ChangeSleepCoef(_upgrade as CoefficientUpgrade);
        }

    }
}
