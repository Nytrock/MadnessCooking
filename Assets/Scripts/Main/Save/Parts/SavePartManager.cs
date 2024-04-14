using UnityEngine;

public abstract class SavePartManager<T> : MonoBehaviour where T: ISaveable
{
    protected IBindable<T>[] _bindables;

    public void LoadData(T data)
    {
        if (_bindables == null) {
            GetBindables();
        }

        foreach (var bindable in _bindables)
            bindable.Bind(data);
    }

    public void SetData(T data)
    {
        if (_bindables == null) {
            GetBindables();
        }

        foreach (var bindable in _bindables)
            bindable.SetData(data);
    }

    protected abstract void GetBindables();
}
