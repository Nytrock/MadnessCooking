using UnityEngine;

public class HoldAdd : MonoBehaviour
{
    [SerializeField] protected HoldAddUI _UI;
    [SerializeField] private float _timeWait;
    private float _progressNow;
    protected float _progressMax;
    protected bool _isWork;
    protected int _count;

    public int Count => _count;

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
        _isWork = newValue;
        _UI.ChangeUI(_isWork);
        if (!_isWork)
            _progressNow = 0;
    }

    private void Update()
    {
        if (!_isWork)
            return;

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
        _count++;
        _UI.SetCountText(_count);
    }
}
