public class FarmDataBinder : LocalDataBinder<GameData, FarmData> {
    protected override void SetData(GameData data) {
        _data = data.Farm;
    }
}
