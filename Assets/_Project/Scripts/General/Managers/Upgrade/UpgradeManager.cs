using UnityEngine;

public class UpgradeManager : SaveableItemManager<BaseUpgrade, GameData>, IBindable<GameData> {
    [SerializeField] private LocalUpgradeManager[] _localUpgradeManagers;

    public override void AddItem(BaseUpgrade upgrade) {
        base.AddItem(upgrade);
        foreach (var manager in _localUpgradeManagers)
            manager.UpgradeAdded(upgrade);
    }

    public override void Bind(GameData data) {
        data.UpgradeManager ??= new();
        _data = data.UpgradeManager;

        BindUpgradeData();
    }

    private void BindUpgradeData() {
        foreach (var manager in _localUpgradeManagers)
            manager.BindUpgradeData();
    }
}
