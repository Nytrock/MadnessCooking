public class OfficeDataBinder : LocalDataBinder<GameData, OfficeData> {
    protected override void SetData(GameData data) {
        _data = data.Office;
    }
}
