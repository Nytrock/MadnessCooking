namespace MadnessCooking.General {
    public class DecorManager : BuyableItemManager<Decor> {
        public override void LoadSave(GameData data) {
            data.General.DecorManager ??= new(_defaultItems);
            _data = data.General.DecorManager;
        }
    }
}
