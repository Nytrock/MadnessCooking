using System;
using UnityEngine;

public abstract class LocalUpgradeManager<TUpgradeData, TData> : MonoBehaviour, IBindable<TData>
    where TUpgradeData : LocalUpgradeData where TData : ISaveable {

    protected MonoBehaviour[] _upgradeablesObjects;
    private IUpgradeable<TUpgradeData>[] _upgradeables;

    protected TUpgradeData _data;

    private void Awake() {
        SetUpgradeableObjects();
        GenerateUpgradeables();
    }

    private void GenerateUpgradeables() {
        _upgradeables = new IUpgradeable<TUpgradeData>[_upgradeablesObjects.Length];
        for (int i = 0; i < _upgradeablesObjects.Length; i++) {
            _upgradeables[i] = _upgradeablesObjects[i].GetComponent<IUpgradeable<TUpgradeData>>();
            if (_upgradeables[i] == null)
                throw new NullReferenceException($"Object {i} don't have type {typeof(TUpgradeData)}");
        }
    }

    public void BingUpgradeData() {
        foreach (var upgradeable in _upgradeables)
            upgradeable.BindUpgrade(_data);
    }

    public void UpgradeAdded(BaseUpgrade upgrade) {
        foreach (var upgradeable in _upgradeables)
            upgradeable.CheckAddedUpgrade(upgrade);
    }

    protected abstract void SetUpgradeableObjects();
    public abstract void Bind(TData data, bool isFileEmpty);
}
