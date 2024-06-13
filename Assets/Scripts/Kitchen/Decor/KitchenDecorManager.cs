public class KitchenDecorManager : SaveableDecorManager<KitchenData> {
    public override void Bind(KitchenData data, bool isFileEmpty) {
        _data = data.DecorData;
        base.Bind(data, isFileEmpty);
    }
}
