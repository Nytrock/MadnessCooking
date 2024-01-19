using System;
using UnityEngine;

public class FarmWell : HoldAdd
{
    [SerializeField] private float _timeWait;

    public event Action<int> WaterAdded;

    protected override void Start()
    {
        _progressMax = _timeWait;
        base.Start();
    }

    protected override void Add()
    {
        base.Add();
        WaterAdded?.Invoke(_count);
    }

    public void SubtractWater()
    {
        _count--;
        _UI.SetCountText(_count);
    }
}
