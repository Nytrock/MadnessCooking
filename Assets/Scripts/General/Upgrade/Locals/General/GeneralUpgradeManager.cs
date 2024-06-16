using UnityEngine;

public class GeneralUpgradeManager : LocalUpgradeManager<GeneralUpgradeData, GeneralData> {
    [SerializeField, RequireInterface(typeof(IUpgradeable<GeneralUpgradeData>))]
    private MonoBehaviour[] _generalUpgradeableObjects;

    protected override void SetUpgradeableObjects() {
        _upgradeablesObjects = _generalUpgradeableObjects;
    }

    public override void Bind(GeneralData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradeData = new();
        _data = data.UpgradeData;
    }
}
