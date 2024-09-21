using System;
using UnityEngine;

public abstract class BaseSettingsPoint : MonoBehaviour {
    public abstract bool IsValueChanged { get; }

    public event Action ValueChanged;

    protected void InvokeValueChanged() {
        ValueChanged?.Invoke();
    }

    public abstract void SetDefaultValue();
    public abstract void Cancel();
    public abstract void Submit();
}
