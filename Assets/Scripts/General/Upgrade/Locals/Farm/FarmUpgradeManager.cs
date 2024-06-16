using UnityEngine;

public class FarmUpgradeManager : LocalUpgradeManager<FarmUpgradeData, FarmData> {
    [SerializeField, RequireInterface(typeof(IUpgradeable<FarmUpgradeData>))]
    private MonoBehaviour[] _farmUpgradeableObjects;

    protected override void SetUpgradeableObjects() {
        _upgradeablesObjects = _farmUpgradeableObjects;
    }

    public override void Bind(FarmData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradeData = new();
        _data = data.UpgradeData;
    }
}
