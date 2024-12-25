using System;
using System.Linq;
using UnityEngine;

public class FpsManager : MonoBehaviour, IBindable<GameSettingsData>, ISettingable<bool> {
    [SerializeField] private int _targetFrameRate = 60;
    [SerializeField] private bool _defaultShow;

    private int _lastFrameIndex;
    private float[] _frameDeltaTimeArray;
    private SettingsPointData<bool> _data;

    public bool DefaultValue => _defaultShow;

    public event Action<bool> FpsShowChanged;

    private void Awake() {
        Application.targetFrameRate = _targetFrameRate;
        _frameDeltaTimeArray = new float[_targetFrameRate];
    }

    private void Update() {
        if (Time.timeScale == 0)
            return;

        _frameDeltaTimeArray[_lastFrameIndex] = Time.deltaTime;
        _lastFrameIndex = (_lastFrameIndex + 1) % _targetFrameRate;
    }

    public float GetFPS() {
        return 1 / (_frameDeltaTimeArray.Sum() / _targetFrameRate);
    }

    public void Bind(GameSettingsData data) {
        data.FpsManager ??= new(DefaultValue);
        _data = data.FpsManager;
    }

    public void LateStart() {
        UpdateValue();
    }

    public void UpdateValue() {
        FpsShowChanged?.Invoke(_data.LastValue);
    }
}
