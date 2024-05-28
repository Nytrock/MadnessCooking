public class OfficeDecorManager : BaseDecorManager<OfficeData> {
    public override void AddDecor(Decor decor) {
        _data.AvailableDecor.Add(decor);
        base.AddDecor(decor);
    }

    public override void Bind(OfficeData data, bool isFileEmpty) {
        _data = data;
        if (isFileEmpty)
            _data.AvailableDecor = new();

        foreach (var decor in _data.AvailableDecor)
            FindAndActivateHolder(decor);
    }
}
