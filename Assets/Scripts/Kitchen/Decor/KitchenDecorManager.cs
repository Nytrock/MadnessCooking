public class KitchenDecorManager : BaseDecorManager<KitchenData>
{
    public override void AddDecor(Decor decor)
    {
        _data.AvailableDecor.Add(decor);
        base.AddDecor(decor);
    }

    public override void Bind(KitchenData data, bool isFileEmpty)
    {
        _data = data;
        foreach (var decor in _data.AvailableDecor)
            FindAndActivateHolder(decor);
    }
}
