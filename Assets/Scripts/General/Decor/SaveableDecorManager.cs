using System.Linq;

public abstract class SaveableDecorManager<TData> : BaseDecorManager, IBindable<TData> where TData : ISaveable {
    protected DecorManagerData _data;

    public override void AddDecor(Decor decor) {
        DecorHolder holder = TryFindHolder(decor);
        if (holder != null) {
            holder.ChangeState(true);
            _data.AddDecor(decor);
        }
    }

    public virtual void Bind(TData data, bool isFileEmpty) {
        foreach (var holder in _decorHolders)
            if (_data.AvailableDecor.Contains(holder.Decor))
                holder.ChangeState(true);
    }
}
