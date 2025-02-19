using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FarmWell : HoldAdd {
    [SerializeField] private VerticalCameraManager _cameraManager;
    [SerializeField] private GameTimeManager _timeManager;
    [SerializeField, Min(0)] private float _pauseTime;
    [SerializeField, Min(1)] private int _turnCount;

    private bool _isPause;
    private float _pauseNeedTime;
    private float _pauseNowTime;

    private Animator _animator;

    public event Action WaterChanged;
    public event Action<float> SpeedChanged;

    protected override void Awake() {
        base.Awake();
        _animator = GetComponent<Animator>();
        _timeManager.TimeSpeedUpdated += UpdateAnimationSpeed;
    }

    public override void LateStart() {
        base.LateStart();
    }

    protected override void Update() {
        if (_isPause) {
            _pauseNowTime += InGameTime.Instance.NormalizedDeltaTime;
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

    private void ChangePause(bool newState) {
        _isPause = newState;

        if (newState) {
            _pauseNowTime = 0;
            _pauseNeedTime = _pauseTime * Data.NowTime / _timeWait;
        }
;
        _animator.SetBool("isPause", newState);
        UpdateAnimationSpeed();
    }

    private void UpdateAnimationSpeed() {
        float period = _turnCount / _timeWait;
        float animationSpeed = _isPause ? -period * Data.NowTime / _pauseTime : period;

        _animator.SetFloat("speed", animationSpeed * InGameTime.Instance.NormalizedTime);
        SpeedChanged?.Invoke(animationSpeed);
    }

    public override void ChangeClickMode(bool newValue) {
        if (!newValue && !Data.IsAuto)
            ChangePause(true);
        ChangeAnimationState(newValue || Data.IsAuto);

        base.ChangeClickMode(newValue);
        _cameraManager.ChangeWorkMode(!newValue);
    }

    public override void SubtractReady() {
        base.SubtractReady();
        WaterChanged?.Invoke();
    }

    private void ChangeAnimationState(bool newValue) {
        _animator.SetBool("isHold", newValue);
        _animator.SetFloat("waitTime", 1 / _timeWait * Data.Speed * InGameTime.Instance.NormalizedTime);
    }

    protected override void UpdateUpgrades() {
        base.UpdateUpgrades();
        if (Data.IsAuto)
            ChangeAnimationState(true);
    }

    public override void Bind(FarmData data) {
        data.FarmWell ??= new(_readyDefaultCount);
        Data = data.FarmWell;
    }
}
