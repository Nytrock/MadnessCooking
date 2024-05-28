using UnityEngine;

public abstract class SavePartManager<TData> : MonoBehaviour where TData : ISaveable {
    protected IBindable<TData>[] _bindables;

    public void LoadData(TData data, bool isFileEmpty) {
        if (_bindables == null) {
            GetBindables();
        }

        foreach (var bindable in _bindables)
            bindable.Bind(data, isFileEmpty);
    }

    protected abstract void GetBindables();
}
