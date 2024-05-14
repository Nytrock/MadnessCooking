using UnityEngine;

public abstract class BaseDecorManager<T> : MonoBehaviour, IBindable<T> where T: ISaveable
{
    [SerializeField] private DecorHolder[] _decorHolders;
    protected T _data;

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

        Debug.LogError("No such decor holder");
    }

    public abstract void Bind(T data, bool isFileEmpty);
}
