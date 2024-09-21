using UnityEngine;

public abstract class UIStyleChanger<TValue, TStyle> : MonoBehaviour {
    [SerializeField] private UIStyleOption<TValue, TStyle>[] _options;

    public void UpdateStyle(TValue nowValue) {
        foreach (var option in _options) {
            if (Equals(option.Value, nowValue)) {
                SetStyle(option.Style);
                return;
            }
        }

        SetStyle(_options[0].Style);
    }

    protected abstract void SetStyle(TStyle style);
}
