using UnityEngine;

public class DecorManager : SaveableItemManager<Decor, GeneralData> {
    [SerializeField] private FatigueManager _fatigueManager;

    public override void AddItem(Decor item) {
        base.AddItem(item);
        _fatigueManager.AddDecorBonus(item);
    }

    public override void Bind(GeneralData data) {
        data.DecorManager ??= new();
        _data = data.DecorManager;
    }
}
