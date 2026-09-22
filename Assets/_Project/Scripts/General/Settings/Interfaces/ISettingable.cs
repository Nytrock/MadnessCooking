namespace MadnessCooking.General {
    public interface ISettingable<TValue> : ISettingable {
        void UpdateValue();
        TValue DefaultValue { get; }
    }

    public interface ISettingable {
        void SetSettings(SettingsData settings);
    }
}
