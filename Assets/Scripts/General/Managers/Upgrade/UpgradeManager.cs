using UnityEngine;

public class UpgradeManager : SaveableItemContainer<BaseUpgrade, GeneralData> {
    [Header("Local upgrade managers")]
    [SerializeField] private GeneralUpgradeManager _generalManager;
    [SerializeField] private CafeUpgradeManager _cafeManager;
    [SerializeField] private KitchenUpgradeManager _kitchenManager;
    [SerializeField] private FarmUpgradeManager _farmManager;
    [SerializeField] private OfficeUpgradeManager _officeManager;

    public void LoadUpgrades() {
        _generalManager.LoadUpgrades();
        _cafeManager.LoadUpgrades();
        _kitchenManager.LoadUpgrades();
        _farmManager.LoadUpgrades();
        _officeManager.LoadUpgrades();
    }

    public override void AddItem(BaseUpgrade upgrade) {
        base.AddItem(upgrade);
        _generalManager.UpgradeAdded(upgrade);
        _cafeManager.UpgradeAdded(upgrade);
        _kitchenManager.UpgradeAdded(upgrade);
        _farmManager.UpgradeAdded(upgrade);
        _officeManager.UpgradeAdded(upgrade);
    }

    public override void Bind(GeneralData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradeManager = new(_defaultItems);
        _data = data.UpgradeManager;
    }
}
