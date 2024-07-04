using System;
using UnityEngine;

[Serializable]
public class SkyManagerData {
    [SerializeField] private Color _nowTopColor;
    [SerializeField] private Color _nowBottomColor;

    [SerializeField] private Color _previousTopColor;
    [SerializeField] private Color _previousBottomColor;

    [SerializeField] private Color _targetTopColor;
    [SerializeField] private Color _targetBottomColor;

    [SerializeField] private float _nowTime;
    [SerializeField] private float _timeStep;
    [SerializeField] private bool _isChanging;

    public bool IsChanging => _isChanging;
    public Color NowTopColor => _nowTopColor;
    public Color NowBottomColor => _nowBottomColor;

    public void SetTimeStep(float timeChanging) {
        _timeStep = 1 / timeChanging;
    }

    public void StartChange(Gradient daytimeGradient) {
        if (_nowTopColor == Color.clear) {
            _nowTopColor = daytimeGradient.Evaluate(0);
            _nowBottomColor = daytimeGradient.Evaluate(1);
            _targetTopColor = daytimeGradient.Evaluate(0);
            _targetBottomColor = daytimeGradient.Evaluate(1);
            return;
        }

        _isChanging = true;
        _nowTime = 0;

        _previousTopColor = _targetTopColor;
        _previousBottomColor = _targetBottomColor;
        _targetTopColor = daytimeGradient.Evaluate(0);
        _targetBottomColor = daytimeGradient.Evaluate(1);
    }

    public void Update() {
        _nowTime += _timeStep * InGameTime.Instance.DeltaTime;

        _nowTopColor = Color.Lerp(_previousTopColor, _targetTopColor, _nowTime);
        _nowBottomColor = Color.Lerp(_previousBottomColor, _targetBottomColor, _nowTime);

        if (_nowTime >= 1)
            _isChanging = false;
    }
}
