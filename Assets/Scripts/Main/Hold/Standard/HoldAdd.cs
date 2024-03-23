using System;
using UnityEngine;

public class HoldAdd : MonoBehaviour, IUpgradeable
{
    [Header("Upgades")]
    [SerializeField] protected BaseUpgrade _unlockUpgrade;
    [SerializeField] protected CoefficientUpgrade _autoWorkUpgrade;

    [Header("Main")]
    [SerializeField] protected HoldAddUI _UI;
    [SerializeField] private float _timeWait;

    private float _progressNow;
    protected float _progressMax;
    protected int _readyCount;
    private float _speed = 1;

    protected bool _isWork;
    protected bool _isUnlocked;
    protected bool _isAuto;

    public int Count => _readyCount;


    protected virtual void Start()
    {
        ResetAll();
        UpdateUpgrades();
    }

    private void ResetAll()
    {
        _isWork = false;
        _progressNow = 0;
        _progressMax = _timeWait;
        _UI.SetSliderMax(_progressMax);
        _UI.ChangeUI(_isWork);
    }

    private void UpdateUpgrades()
    {
        gameObject.SetActive(_isUnlocked);
        if (_isAuto)
            _speed = _autoWorkUpgrade.Coefficient;
    }

    public virtual void ChangeWorkMode(bool newValue)
    {
        _UI.ChangeUI(newValue);
        if (_isAuto)
            return;

        _isWork = newValue;
        if (!_isWork)
            _progressNow = 0;
    }

    private void Update()
    {
        if (!_isWork && !_isAuto)
            return;

        UpdateTimer();
    }

    protected virtual void UpdateTimer()
    {
        if (_progressNow < _progressMax) {
            _progressNow += Time.deltaTime * _speed;
        } else {
            Add();
        }

        _UI.SetSliderValue(_progressNow);
    }

    protected virtual void Add()
    {
        _progressNow = 0;
        _readyCount++;
        _UI.SetCountText(_readyCount);
    }

    public virtual void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _unlockUpgrade)
            _isUnlocked = true;
        else if (upgrade == _autoWorkUpgrade)
            _isAuto = true;

        UpdateUpgrades();
    }
}
