using System;
using System.Collections;
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
        StartCoroutine(nameof(TextAnimation));
    }

    private IEnumerator TextAnimation() {
        while (_lastCharIndex < _targetText.Length) {
            _lastCharIndex++;
            _text.text = _targetText[.._lastCharIndex];
            TextUpdated?.Invoke();
            yield return new WaitForSeconds(1 / _speed * FpsManager.REFERENCE_FPS * Time.deltaTime);
        }

        StopAnimation();
    }

    public void StopAnimation() {
        if (string.IsNullOrEmpty(_targetText) || !_isAnimated)
            return;

        _isAnimated = false;
        _text.text = _targetText;
        TextUpdated?.Invoke();
        StopCoroutine(nameof(TextAnimation));
    }

    private void ForceStopAnimation() {
        _isAnimated = false;
        TextUpdated?.Invoke();
        StopCoroutine(nameof(TextAnimation));
    }
}
