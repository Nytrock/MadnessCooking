using UnityEngine;

public class DecorManager : BuyableItemManager<Decor>, IBindable<GeneralData> {
    [SerializeField] private LocationDecorManager[] _localDecorManagers;

    public override void AddItem(Decor item) {
        base.AddItem(item);
        AddDecorToLocalManagers(item);
    }

    public void Bind(GeneralData data, bool isFileEmpty) {
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
