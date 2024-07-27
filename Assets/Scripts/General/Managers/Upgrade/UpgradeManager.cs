using UnityEngine;

public class UpgradeManager : SaveableItemManager<BaseUpgrade, GameData>, ILoadable<GameData> {
    [SerializeField] private LocalUpgradeManager[] _localUpgradeManagers;

    public override void AddItem(BaseUpgrade upgrade) {
        base.AddItem(upgrade);
        foreach (var manager in _localUpgradeManagers)
            manager.UpgradeAdded(upgrade);
    }

    public void Load(GameData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.UpgradeManager = new();
        _data = data.UpgradeManager;

        BindUpgradeData();
        base.Bind(data, isFileEmpty);
    }

    private void BindUpgradeData() {
        foreach (var manager in _localUpgradeManagers)
            manager.BindUpgradeData();
    }
}
