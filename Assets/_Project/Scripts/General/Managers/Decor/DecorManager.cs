public class DecorManager : SaveableItemManager<Decor, GeneralData> {
    public override void Bind(GeneralData data) {
        data.DecorManager ??= new();
        _data = data.DecorManager;
    }
}
