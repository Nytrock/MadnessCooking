using UnityEngine;

public abstract class SavePartManager<T> : MonoBehaviour where T: ISaveable
{
    protected IBindable<T>[] _bindables;

    public void LoadData(T data, bool isFileEmpty)
    {
        if (_bindables == null) {
            GetBindables();
        }

        foreach (var bindable in _bindables)
            bindable.Bind(data, isFileEmpty);
    }

    protected abstract void GetBindables();
}
