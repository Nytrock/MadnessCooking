public class CafeDataLoader : LocalDataLoader<GameData, CafeData> {
    protected override void SetData(GameData data) {
        _data = data.Cafe;
    }
}
