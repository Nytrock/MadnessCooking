using System;
using UnityEngine;

public class FpsManager : MonoBehaviour, IBindable<GameSettingsData>, ISettingable<bool> {
    public const int REFERENCE_FPS = 60;
    public static float NORMALIZED_DELTA_TIME => REFERENCE_FPS * Time.deltaTime;
    public static float REFERENCE_DELTA_TIME => 1f / REFERENCE_FPS;

    [SerializeField] private int _frameRatePrecision = 60;
    [SerializeField] private bool _defaultShow;

    private int _lastFrameIndex = 0;
    private float[] _frameDeltaTimeArray;
    private SettingsPointData<bool> _data;

    public bool DefaultValue => _defaultShow;

    public event Action<bool> FpsShowChanged;

    private void Awake() {
        QualitySettings.vSyncCount = 1;
        _frameDeltaTimeArray = new float[_frameRatePrecision];
    }

    private void Update() {
        if (Time.timeScale == 0)
            return;

        _frameDeltaTimeArray[_lastFrameIndex] = Time.deltaTime;
        _lastFrameIndex = (_lastFrameIndex + 1) % _frameRatePrecision;
    }

    public float GetFPS() {
        int framesCount = 0;
        float framesSum = 0;

        foreach (var frame in _frameDeltaTimeArray) {
            if (frame == 0)
                break;
            framesCount++;
            framesSum += frame;
        }

        return 1 / (framesSum / framesCount);
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
