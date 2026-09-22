namespace MadnessCooking.General {
    public interface IUpgradeable {
        void SetUpgradeData(GameData gameData);
        void CheckAddedUpgrade(BaseUpgrade upgrade);
    }
}
