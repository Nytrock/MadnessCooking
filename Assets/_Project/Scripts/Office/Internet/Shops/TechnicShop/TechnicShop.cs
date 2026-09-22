using MadnessCooking.General;

namespace MadnessCooking.Office {
    public class TechnicShop : BaseInstantShop<Technic> {
        public override void LoadSave(GameData data) {
            data.Office.TechnicShop ??= new(_defaultItemsToBuy);
            _data = data.Office.TechnicShop;
            base.LoadSave(data);
        }
    }
}
