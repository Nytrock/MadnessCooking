using System;
using UnityEngine;

namespace MadnessCooking.General {
    public class FpsManager : MonoBehaviour, ISettingable<bool> {
        public const int REFERENCE_FPS = 60;
        public static float NORMALIZED_DELTA_TIME => REFERENCE_FPS * Time.deltaTime;
        public static float REFERENCE_DELTA_TIME => 1f / REFERENCE_FPS;

        [SerializeField] private int _frameRatePrecision = 60;
        [SerializeField] private bool _defaultShow;

        private int _lastFrameIndex = 0;
        private float[] _frameDeltaTimeArray;
        private float _framesSum = 0;
        private int _framesCount = 0;
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

            _framesSum -= _frameDeltaTimeArray[_lastFrameIndex];
            _frameDeltaTimeArray[_lastFrameIndex] = Time.deltaTime;
            _framesSum += _frameDeltaTimeArray[_lastFrameIndex];

            _lastFrameIndex = (_lastFrameIndex + 1) % _frameRatePrecision;
            _framesCount = Mathf.Min(_framesCount + 1, _frameRatePrecision);
        }

        public float GetFPS() {
            return 1 / (_framesSum / _framesCount);
        }

        public void SetSettings(SettingsData data) {
            data.GameSettings.FpsManager ??= new(DefaultValue);
            _data = data.GameSettings.FpsManager;
        }

        public void LateStart() {
            UpdateValue();
        }

        public void UpdateValue() {
            FpsShowChanged?.Invoke(_data.LastValue);
        }
    }
}
