using UnityEngine;

public class KitchenUpgradeManager : LocalUpgradeManager<KitchenUpgradeData, KitchenData> {
    [SerializeField, RequireInterface(typeof(IUpgradeable<KitchenUpgradeData>))]
    private MonoBehaviour[] _kitchenUpgradeableObjects;

    protected override void SetUpgradeableObjects() {
        _upgradeablesObjects = _kitchenUpgradeableObjects;
    }

    public override void Bind(KitchenData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradeData = new();
        _data = data.UpgradeData;
    }
}
