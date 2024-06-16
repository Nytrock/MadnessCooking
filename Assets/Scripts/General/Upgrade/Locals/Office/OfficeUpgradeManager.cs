using UnityEngine;

public class OfficeUpgradeManager : LocalUpgradeManager<OfficeUpgradeData, OfficeData> {
    [SerializeField, RequireInterface(typeof(IUpgradeable<OfficeUpgradeData>))]
    private MonoBehaviour[] _officeUpgradeableObjects;

    protected override void SetUpgradeableObjects() {
        _upgradeablesObjects = _officeUpgradeableObjects;
    }

    public override void Bind(OfficeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradeData = new();
        _data = data.UpgradeData;
    }
}
