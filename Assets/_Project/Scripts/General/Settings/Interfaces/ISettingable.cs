namespace MadnessCooking.General {
    public interface ISettingable<TValue> {
        void UpdateValue();
        TValue DefaultValue { get; }
    }
}
