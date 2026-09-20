namespace MadnessCooking.General {
    public interface ISettingableWithOptions : ISettingable<int> {
        int OptionsCount { get; }
    }
}
