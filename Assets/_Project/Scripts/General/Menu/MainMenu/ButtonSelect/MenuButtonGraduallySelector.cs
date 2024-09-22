using UnityEngine;

public class MenuButtonGraduallySelector : MenuButtonSelector {
    [SerializeField, Min(0.01f)] private float _speed = 1;
    private float _moveProgress;
    private bool _isMove;

    protected override void Update() {
        base.Update();

        if (!_isMove)
            return;

        UpdatePosition();
    }

    private void UpdatePosition() {
        _moveProgress += _speed * Time.unscaledDeltaTime;
        _nowRect.sizeDelta = Vector2.Lerp(_nowRect.sizeDelta, _targetRect.sizeDelta, _moveProgress);
        _nowRect.position = Vector3.Lerp(_nowRect.position, _targetRect.position, _moveProgress);

        if (_moveProgress >= 1)
            _isMove = false;
    }

    protected override void ChangePosition() {
        _moveProgress = 0;
        _isMove = true;
    }
}
