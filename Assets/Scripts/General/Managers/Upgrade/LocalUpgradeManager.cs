using AYellowpaper;
using UnityEngine;

public abstract class LocalUpgradeManager<TUpgradeData, TData> : MonoBehaviour, IBindable<TData>
    where TUpgradeData : LocalUpgradeData where TData : ISaveable {

    [SerializeField] private InterfaceReference<IUpgradeable<TUpgradeData>>[] _upgradeables;

    protected TUpgradeData _data;

    public void BindUpgradeData() {
        foreach (var upgradeable in _upgradeables)
            upgradeable.Value.BindUpgrade(_data);
    }

    public void UpgradeAdded(BaseUpgrade upgrade) {
        foreach (var upgradeable in _upgradeables)
            upgradeable.Value.CheckAddedUpgrade(upgrade);
    }

    public abstract void Bind(TData data, bool isFileEmpty);
}
