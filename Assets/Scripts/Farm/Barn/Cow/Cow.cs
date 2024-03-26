using UnityEngine;

public class Cow : HoldDoubleAdd
{
    [SerializeField] private WheatManager _wheatManager;
    [SerializeField] private BarnFridge _barnFridge;

    protected override void Start()
    {
        _wheatManager.CowWheatChanged += SetWheatNum;
        _barnFridge.MilkChanged += UpdateMilkCount;
        base.Start();
    }

    private void SetWheatNum(int num)
    {
        _materialCount = num;
        _UI.SetCountText(_materialCount, _readyCount);
    }

    private void UpdateMilkCount(int count)
    {
        if (count > 0)
            return;

        _readyCount = count;
        _UI.SetCountText(_materialCount, count);
    }

    protected override void Add()
    {
        base.Add();
        _wheatManager.SubstractWheat(typeof(Cow));
        _barnFridge.AddMilk(1);
    }
}
