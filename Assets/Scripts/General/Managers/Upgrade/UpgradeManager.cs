using UnityEngine;

public class UpgradeManager : SaveableItemManager<BaseUpgrade, GameData> {
    [Header("Local upgrade managers")]
    [SerializeField] private GeneralUpgradeManager _generalManager;
    [SerializeField] private CafeUpgradeManager _cafeManager;
    [SerializeField] private KitchenUpgradeManager _kitchenManager;
    [SerializeField] private FarmUpgradeManager _farmManager;
    [SerializeField] private OfficeUpgradeManager _officeManager;

    public override void AddItem(BaseUpgrade upgrade) {
        base.AddItem(upgrade);
        _generalManager.UpgradeAdded(upgrade);
        _cafeManager.UpgradeAdded(upgrade);
        _kitchenManager.UpgradeAdded(upgrade);
        _farmManager.UpgradeAdded(upgrade);
        _officeManager.UpgradeAdded(upgrade);
    }

    public override void Bind(GameData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradeManager = new();
        _data = data.UpgradeManager;
        BindUpgradeData();
        base.Bind(data, isFileEmpty);
    }

    private void BindUpgradeData() {
        _generalManager.BindUpgradeData();
        _cafeManager.BindUpgradeData();
        _kitchenManager.BindUpgradeData();
        _farmManager.BindUpgradeData();
        _officeManager.BindUpgradeData();
    }
}
