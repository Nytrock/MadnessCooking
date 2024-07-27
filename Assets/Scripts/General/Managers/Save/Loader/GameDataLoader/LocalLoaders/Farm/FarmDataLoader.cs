public class FarmDataLoader : LocalDataLoader<GameData, FarmData> {
    protected override void SetData(GameData data) {
        _data = data.Farm;
    }
}
