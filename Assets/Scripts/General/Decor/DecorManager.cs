using UnityEngine;

public class DecorManager : SaveableItemManager<Decor, GeneralData> {
    [SerializeField] private LocalDecorManager[] _localDecorManagers;

    public override void AddItem(Decor item) {
        base.AddItem(item);
        AddDecorToLocalManagers(item);
    }

    public override void Bind(GeneralData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.DecorManager = new(_defaultItems);
        _data = data.DecorManager;

        foreach (var decor in _data.AvailableItems)
            AddDecorToLocalManagers(decor);
    }

    private void AddDecorToLocalManagers(Decor decor) {
        foreach (var manager in _localDecorManagers)
            manager.AddDecor(decor);
    }
}
