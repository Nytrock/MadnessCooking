public class GeneralDataLoader : LocalDataLoader<GameData, GeneralData> {
    protected override void SetData(GameData data) {
        _data = data.General;
    }
}
