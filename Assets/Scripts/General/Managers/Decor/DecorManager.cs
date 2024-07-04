using UnityEngine;

public class DecorManager : SaveableItemContainer<Decor, GeneralData> {
    [SerializeField] private LocalDecorManager[] _localDecorManagers;
    [SerializeField] private FatigueManager _fatigueManager;

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
        _fatigueManager.AddDecorBonus(decor);
        foreach (var manager in _localDecorManagers)
            manager.AddDecor(decor);
    }
}
