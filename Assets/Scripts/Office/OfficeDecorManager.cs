public class OfficeDecorManager : SaveableDecorManager<OfficeData> {
    public override void Bind(OfficeData data, bool isFileEmpty) {
        _data = data.DecorData;
        base.Bind(data, isFileEmpty);
    }
}
