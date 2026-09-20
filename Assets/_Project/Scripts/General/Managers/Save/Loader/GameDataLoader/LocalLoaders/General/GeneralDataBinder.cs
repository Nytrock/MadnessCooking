namespace MadnessCooking.General {
    public class GeneralDataBinder : LocalDataBinder<GameData, GeneralData> {
        protected override void SetData(GameData data) {
            _data = data.General;
        }
    }
}
