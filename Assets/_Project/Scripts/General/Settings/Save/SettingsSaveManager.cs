namespace MadnessCooking.General {
    public class SettingsSaveManager : SaveManager<SettingsData> {
        protected override string FileName => "settings";
    }
}
