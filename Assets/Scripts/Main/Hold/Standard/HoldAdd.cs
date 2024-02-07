using UnityEngine;

public class HoldAdd : MonoBehaviour
{
    [SerializeField] protected HoldAddUI _UI;
    [SerializeField] private float _timeWait;
    private float _progressNow;
    protected float _progressMax;
    protected bool _isWork;
    protected int _readyCount;

    protected bool _isAuto;

    public int Count => _readyCount;

    protected virtual void Start()
    {
        _isWork = false;
        _progressNow = 0;
        _progressMax = _timeWait;
        _UI.SetSliderMax(_progressMax);
        _UI.ChangeUI(_isWork);
    }

    public virtual void ChangeWorkMode(bool newValue)
    {
        if (_isAuto)
            return;

        _isWork = newValue;
        _UI.ChangeUI(_isWork);
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
            _progressNow += Time.deltaTime;
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
}
