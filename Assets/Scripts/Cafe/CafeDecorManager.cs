public class CafeDecorManager : SaveableDecorManager<CafeData> {
    public override void Bind(CafeData data, bool isFileEmpty) {
        _data = data.Decor;
        base.Bind(data, isFileEmpty);
    }
}
