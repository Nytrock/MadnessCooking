public class CafeDataBinder : LocalDataBinder<GameData, CafeData> {
    protected override void SetData(GameData data) {
        _data = data.Cafe;
    }
}
