public class KitchenDataLoader : LocalDataLoader<GameData, KitchenData> {
    protected override void SetData(GameData data) {
        _data = data.Kitchen;
    }
}
