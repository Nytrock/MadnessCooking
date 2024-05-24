using System;
using UnityEngine;

public abstract class BaseDecorManager<TData> : MonoBehaviour, IBindable<TData> where TData: ISaveable
{
    [SerializeField] private DecorHolder[] _decorHolders;
    protected TData _data;

    private void Awake()
    {
        foreach (var holder in _decorHolders)
            holder.ChangeState(false);
    }

    public virtual void AddDecor(Decor decor)
    {
        FindAndActivateHolder(decor);
    }

    protected void FindAndActivateHolder(Decor decor)
    {
        foreach (var holder in _decorHolders) {
            if (holder.Decor == decor) {
                holder.ChangeState(true);
                return;
            }
        }

        throw new ArgumentNullException("No such decor holder");
    }

    public abstract void Bind(TData data, bool isFileEmpty);
}
