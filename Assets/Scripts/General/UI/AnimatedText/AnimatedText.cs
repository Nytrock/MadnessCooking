using UnityEngine;

public class AnimatedText : LocalizedText {
    [SerializeField, Min(0)] private float _speed;

    private string _targetText;
    private int _lastCharIndex;
    private bool _isAnimated;

    public bool IsAnimated => _isAnimated;

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
    }

    public void StopAnimation() {
        _isAnimated = false;
        _text.text = _targetText;
        CancelInvoke(nameof(UpdateTextAnimation));
    }

    private void ForceStopAnimation() {
        _isAnimated = false;
        CancelInvoke(nameof(UpdateTextAnimation));
    }
}
