using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FarmWell : HoldAdd {
    [SerializeField] private VerticalCameraManager _cameraManager;
    [SerializeField, Min(0)] private float _pauseTime;
    [SerializeField, Min(1)] private int _turnCount;

    private bool _isPause;
    private float _pauseNeedTime;
    private float _pauseNowTime;

    private Animator _animator;

    public event Action WaterChanged;

    protected override void Awake() {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    protected override void Update() {
        if (_isPause) {
            _pauseNowTime += Time.deltaTime;
            if (_pauseNowTime > _pauseNeedTime)
                ChangePause(false);
            return;
        }

        base.Update();
    }

    protected override void AddReady() {
        ChangePause(true);
        base.AddReady();
        WaterChanged?.Invoke();
    }

    private void ChangePause(bool newValue) {
        _isPause = newValue;

        if (newValue) {
            _pauseNowTime = 0;
            _pauseNeedTime = _pauseTime * Data.NowTime / _timeWait;
        }

        float period = _turnCount / _timeWait;
        _animator.SetBool("isPause", newValue);
        _animator.SetFloat("speed", newValue ? -period * Data.NowTime / _pauseTime : period);
    }

    public override void ChangeWorkMode(bool newValue) {
        if (!newValue && !Data.IsAuto)
            ChangePause(true);
        ChangeAnimationState(newValue || Data.IsAuto);

        base.ChangeWorkMode(newValue);
        _cameraManager.ChangeWorkMode(!newValue);
    }

    public override void SubtractReady() {
        base.SubtractReady();
        WaterChanged?.Invoke();
    }

    private void ChangeAnimationState(bool newValue) {
        _animator.SetBool("isHold", newValue);
        _animator.SetFloat("waitTime", 1 / _timeWait * Data.Speed);
    }

    protected override void UpdateUpgrades() {
        base.UpdateUpgrades();
        if (Data.IsAuto)
            ChangeAnimationState(true);
    }

    public override void Bind(FarmData data) {
        data.FarmWell ??= new(_readyDefaultCount);
        Data = data.FarmWell;
        base.Bind(data);
    }
}
