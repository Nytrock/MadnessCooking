namespace MadnessCooking.General {
    public class KitchenDataBinder : LocalDataBinder<GameData, KitchenData> {
        protected override void SetData(GameData data) {
            _data = data.Kitchen;
        }
    }
}
