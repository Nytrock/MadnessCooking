public class HoldDoubleAdd : HoldAdd
{
    protected int _materialCount;

    protected override void Start()
    {
        _UI.SetCountText(_materialCount, _readyCount);
        base.Start();
    }

    protected override void UpdateTimer()
    {
        if (_materialCount == 0)
            return;

        base.UpdateTimer();
    }

    public override void ChangeWorkMode(bool newValue)
    {
        if (_materialCount == 0) {
            _UI.ChangeUI(newValue);
            return;
        }

        base.ChangeWorkMode(newValue);
    }

    protected override void Add()
    {
        _materialCount--;
        _UI.SetCountText(_materialCount, _readyCount);
        base.Add();

        if (_materialCount == 0)
            _isWork = false;
    }
}
