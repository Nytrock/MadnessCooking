using AYellowpaper;
using UnityEngine;

public abstract class SettingsPoint<TValue> : BaseSettingsPoint {
    [SerializeField] protected InterfaceReference<ISettingable<TValue>> _settingable;

    protected SettingsPointData<TValue> _data;

    public override bool IsValueChanged => _data.IsValueChanged;

    public virtual void ChangeValue(TValue newValue) {
        _data.ChangeValue(newValue);
        InvokeValueChanged();
        UpdateState();
    }

    public override void SetDefaultValue() {
        ChangeValue(_settingable.Value.DefaultValue);
        UpdateState();
    }

    public override void Cancel() {
        _data.CancelChanginng();
        UpdateState();
    }

    public override void Submit() {
        _data.SubmitChanginng();
    }

    protected virtual void UpdateState() {
        _settingable.Value.UpdateValue();
    }
}
