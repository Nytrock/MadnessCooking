public class OfficeDecorManager : BaseDecorManager<OfficeData>
{
    public override void AddDecor(Decor decor)
    {
        _data.HaveDecor.Add(decor);
        base.AddDecor(decor);
    }

    public override void Bind(OfficeData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty)
            _data.HaveDecor = new();

        foreach (var decor in _data.HaveDecor)
            FindAndActivateHolder(decor);
    }
}
