namespace MadnessCooking.General {
    public abstract class SettingsPoint<TValue> : BaseSettingsPoint {
        protected ISettingable<TValue> _settingable;
        protected SettingsPointData<TValue> _data;

        public override bool IsValueChanged => _data.IsValueChanged;

        public void SetSettingable(ISettingable<TValue> settingable) {
            _settingable = settingable;
        }

        public virtual void ChangeValue(TValue newValue) {
            _data.ChangeValue(newValue);
            InvokeValueChanged();
            UpdateState();
        }

        public override void SetDefaultValue() {
            ChangeValue(_settingable.DefaultValue);
            UpdateState();
        }

        public override void Cancel() {
            _data.CancelChanginng();
            UpdateState();
        }

        public override void Submit() {
            _data.SubmitChanging();
        }

        public override void UpdateState() {
            _settingable.UpdateValue();
        }

        public void SetData(SettingsPointData<TValue> data) {
            _data = data;
        }
    }
}
