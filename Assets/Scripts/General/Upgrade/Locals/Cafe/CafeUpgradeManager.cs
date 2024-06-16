using UnityEngine;

public class CafeUpgradeManager : LocalUpgradeManager<CafeUpgradeData, CafeData> {
    [SerializeField, RequireInterface(typeof(IUpgradeable<CafeUpgradeData>))]
    private MonoBehaviour[] _cafeUpgradeableObjects;

    protected override void SetUpgradeableObjects() {
        _upgradeablesObjects = _cafeUpgradeableObjects;
    }

    public override void Bind(CafeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradeData = new();
        _data = data.UpgradeData;
    }
}
