public class OfficeDataLoader : LocalDataLoader<GameData, OfficeData> {
    protected override void SetData(GameData data) {
        _data = data.Office;
    }
}
