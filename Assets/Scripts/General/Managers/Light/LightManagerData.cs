using System;
using UnityEngine;

[Serializable]
public class LightManagerData {
    [SerializeField] private Color _previousLight;
    [SerializeField] private Color _nowLight;
    [SerializeField] private Color _targetLight;

    [SerializeField] private float _nowTime;
    [SerializeField] private float _timeStep;
    [SerializeField] private bool _isChanging;

    public Color NowLight => _nowLight;
    public bool IsChanging => _isChanging;

    public void SetTimeStep(float timeChanging) {
        _timeStep = 1 / timeChanging;
    }

    public void StartChange(DaytimeLight light) {
        if (_nowLight == Color.clear) {
            _nowLight = light.LightColor;
            _targetLight = light.LightColor;
            return;
        }

        _isChanging = true;
        _nowTime = 0;

        _previousLight = _targetLight;
        _targetLight = light.LightColor;
    }

    public void Update() {
        _nowTime += _timeStep * InGameTime.Instance.DeltaTime;

        _nowLight = Color.Lerp(_previousLight, _targetLight, _nowTime);

        if (_nowTime >= 1)
            _isChanging = false;
    }
}
