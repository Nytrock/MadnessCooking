using System;

namespace MadnessCooking.General {
    public class IntSettingsPoint : SettingsPoint<int> {
        private int _optionsCount;

        protected virtual void Awake() {
            if (_settingable.Value as ISettingableWithOptions is null)
                throw new ArgumentNullException($"{_settingable} isn't ISettingableWithOptions");

            _optionsCount = (_settingable.Value as ISettingableWithOptions).OptionsCount;
        }

        public void NextOption() {
            ChangeValue((_data.LastValue + 1) % _optionsCount);
        }

        public void PreviousOption() {
            if (_data.LastValue == 0)
                ChangeValue(_optionsCount - 1);
            else
                ChangeValue(_data.LastValue - 1);
        }
    }
}
