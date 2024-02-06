using System;

public class Puncher : HoldAdd
{
    private int _shitCount;

    public event Action<int> FertilizeAdded;

    protected override void Start()
    {
        _UI.SetCountText(_shitCount, _count);
        base.Start();
    }

    public void SubtractFertilize()
    {
        _count--;
        _UI.SetCountText(_shitCount, _count);
    }

    public override void ChangeWorkMode(bool newValue)
    {
        if (_shitCount == 0) {
            _UI.ChangeUI(newValue);
            return;
        }

        base.ChangeWorkMode(newValue);
    }

    protected override void Add()
    {
        _shitCount--;
        _UI.SetCountText(_shitCount, _count);
        base.Add();
        FertilizeAdded?.Invoke(_count);

        if (_shitCount == 0)
            _isWork = false;
    }
}
