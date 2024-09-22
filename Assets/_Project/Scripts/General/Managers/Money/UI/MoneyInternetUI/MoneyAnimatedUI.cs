using TMPro;
using UnityEngine;

public class MoneyAnimatedUI : MoneyBaseUI {
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private Animator _animator;
    [SerializeField, Min(0.0001f)] private float _animationDuration;

    private bool _isPlaying;
    private float _animationNow;
    private int _animationCount;

    private void Update() {
        if (!_isPlaying)
            return;

        _animationNow += Time.deltaTime / _animationDuration;
        _animationCount = (int)Mathf.Lerp(_oldCount, _nowCount, _animationNow);
        _countText.text = "$" + CountConverter.ToCount(_animationCount);

        if (_animationNow >= 1)
            StopAnimation();
    }

    protected override void StartAnimation() {
        _isPlaying = true;
        if (_animator.isActiveAndEnabled) {
            _animator.SetBool("isGet", _isCountAdded);
            _animator.SetBool("isLost", !_isCountAdded);
        }

        _animationCount = _oldCount;
        _animationNow = 0;
    }

    private void StopAnimation() {
        _isPlaying = false;
        if (!_animator.isActiveAndEnabled)
            return;

        _animator.SetBool("isGet", false);
        _animator.SetBool("isLost", false);
    }
}
