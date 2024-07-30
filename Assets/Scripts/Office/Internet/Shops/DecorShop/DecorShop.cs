using System;
using UnityEngine;

public class DecorShop : BaseInstantShop<Decor, OfficeData> {
    [SerializeField] private DecorLocationImage[] _decorLocations;

    protected override void SortItems() {
        Func<Decor, int> sortMethod = (decor) => decor.Price + ((int)decor.Location * 10000);
        _data.OrderItems(sortMethod);
    }

    protected override GrayscaleImageData GenerateSideInfo(Decor decor) {
        foreach (var location in _decorLocations)
            if (location.Location == decor.Location)
                return new(location.Sprite, false);
        return null;
    }

    public override void Bind(OfficeData data) {
        data.DecorShop ??= new(_defaultItemsToBuy);
        _data = data.DecorShop;
        base.Bind(data);
    }
}
