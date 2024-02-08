using UnityEngine;

[RequireComponent(typeof(TechnicHolder))]
public class TechnicWaiter : MonoBehaviour
{
    protected float _nowTime;
    protected float _needTime;

    protected float _speedMultiplier = 1f;
    protected bool _isWorking;

    protected TechnicHolder _holder;

    public float NowTime => _nowTime;
    public float NeedTime => _needTime;

    private void Awake()
    {
        _holder = GetComponent<TechnicHolder>();
    }

    public virtual void StartWork(float needTime)
    {
        _isWorking = true;
        _needTime = needTime;
    }

    private void Update()
    {
        if (!_isWorking)
            return;

        if (_nowTime < _needTime)
            _nowTime += Time.deltaTime * _speedMultiplier;
        else
            EndWork();
    }

    protected virtual void EndWork()
    {
        _isWorking = false;
        _nowTime = 0f;
    }
}
