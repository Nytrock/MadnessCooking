using System;
using UnityEngine;

public class UpgradeManager : BuyableItemManager<BaseUpgrade> {
    [RequireInterface(typeof(IUpgradeable)), SerializeField]
    private MonoBehaviour[] _upgradeablesObjects;
    private IUpgradeable[] _upgradeables;

    private void Awake() {
        _upgradeables = new IUpgradeable[_upgradeablesObjects.Length];
        for (int i = 0; i < _upgradeablesObjects.Length; i++) {
            _upgradeables[i] = _upgradeablesObjects[i].GetComponent<IUpgradeable>();
            if (_upgradeables[i] == null)
                throw new NullReferenceException($"Object {i} don't have type {typeof(FarmData)}");
        }
    }

    public override void AddItem(BaseUpgrade upgrade) {
        foreach (var upgradeable in _upgradeables) {
            upgradeable.CheckUpgrade(upgrade);
        }
        base.AddItem(upgrade);
    }
}
