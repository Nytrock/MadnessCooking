using System;
using UnityEngine;

public abstract class BaseSettingsPoint : MonoBehaviour {
    public abstract bool IsValueChanged { get; }

    public event Action ValueChanged;

    protected void InvokeValueChanged() {
        ValueChanged?.Invoke();
    }

    public void LateStart() {
        UpdateState();
    }

    protected abstract void UpdateState();
    public abstract void SetDefaultValue();
    public abstract void Cancel();
    public abstract void Submit();
}
