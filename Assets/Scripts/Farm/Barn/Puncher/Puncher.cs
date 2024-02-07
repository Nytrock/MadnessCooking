using System;

public class Puncher : HoldDoubleAdd
{
    public event Action<int> FertilizeChanged;

    public void AddShit()
    {
        _materialCount++;
        _UI.SetCountText(_materialCount, _readyCount);
    }

    public void SubtractFertilize()
    {
        _readyCount--;
        _UI.SetCountText(_materialCount, _readyCount);
        FertilizeChanged?.Invoke(_readyCount);
    }

    protected override void Add()
    {
        base.Add();
        FertilizeChanged?.Invoke(_readyCount);
    }
}
