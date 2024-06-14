using UnityEngine;

public class DecorShop : BaseInstantShop<Decor, OfficeData> {
    [SerializeField] private BaseDecorManager[] _decorManagers;

    public override void BuyItem(Decor decor) {
        FatigueManager.Instance.AddDecorBonus(decor);
        foreach (var decorManager in _decorManagers)
            decorManager.AddDecor(decor);
        base.BuyItem(decor);
    }

    public override void Bind(OfficeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.DecorShop = new(_defaultItemsToBuy);
        _data = data.DecorShop;
        base.Bind(data, isFileEmpty);
    }
}
