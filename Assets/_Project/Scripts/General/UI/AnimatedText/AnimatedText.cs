using System;
using UnityEngine;

public class AnimatedText : LocalizedText {
    [SerializeField, Min(0)] private float _speed;

    private string _targetText;
    private int _lastCharIndex;
    private bool _isAnimated;

    public bool IsAnimated => _isAnimated;

    public event Action TextUpdated;

    public override void UpdateText() {
        base.UpdateText();
        _targetText = _text.text;
    }

    public override void SetText(string text) {
        base.SetText(text);
        if (_isAnimated)
            ForceStopAnimation();

        _isAnimated = true;
        _lastCharIndex = 0;
        InvokeRepeating(nameof(UpdateTextAnimation), 0, 1 / _speed * Time.deltaTime);
    }

    private void UpdateTextAnimation() {
        _lastCharIndex++;
        if (_lastCharIndex >= _targetText.Length) {
            StopAnimation();
            return;
        }

        _text.text = _targetText[.._lastCharIndex];
        TextUpdated?.Invoke();
    }

    public void StopAnimation() {
        if (string.IsNullOrEmpty(_targetText) || !_isAnimated)
            return;

        _isAnimated = false;
        _text.text = _targetText;
        TextUpdated?.Invoke();
        CancelInvoke(nameof(UpdateTextAnimation));
    }

    private void ForceStopAnimation() {
        _isAnimated = false;
        TextUpdated?.Invoke();
        CancelInvoke(nameof(UpdateTextAnimation));
    }
}
