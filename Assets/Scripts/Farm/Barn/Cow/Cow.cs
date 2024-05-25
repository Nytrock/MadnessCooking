public class Cow : NeedHoldAdd
{
    private NeedHoldAddData _flourMillData;
    private FarmData _data;

    protected override void Add()
    {
        if (!_data.IsWheatDistributing)
            _flourMillData.MaterialCount--;
        base.Add();
    }

    public override void Bind(FarmData data, bool isFileEmpty)
    {
        HoldData = data.Cow;
        _flourMillData = data.FlourMill;
        _data = data;
        base.Bind(data, isFileEmpty);
    }
}
