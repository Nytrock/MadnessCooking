using MadnessCooking.General;
using System;
using UnityEngine;

namespace MadnessCooking.Office {
    public class DecorShop : BaseInstantShop<Decor> {
        [SerializeField] private LocationData[] _decorLocations;

        protected override void SortItems() {
            Func<Decor, int> sortMethod = (decor) => decor.Price + ((int)decor.Location * 10000);
            _data.OrderItems(sortMethod);
        }

        protected override BuyPanelSideInfoData GenerateSideInfo(Decor decor) {
            foreach (var location in _decorLocations)
                if (location.Location == decor.Location)
                    return new(location.Sprite, false, location.Name);
            return null;
        }

        public override void LoadSave(GameData data) {
            data.Office.DecorShop ??= new(_defaultItemsToBuy);
            _data = data.Office.DecorShop;
            base.LoadSave(data);
        }
    }
}
