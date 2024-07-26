using AYellowpaper;
using UnityEngine;

public abstract class LocalDataLoader<TData> : MonoBehaviour
    where TData : ISaveable {

    [SerializeField] protected InterfaceReference<IBindable<TData>>[] _bindables;

    public void LoadData(TData data, bool isFileEmpty) {
        foreach (var bindable in _bindables)
            bindable.Value.Bind(data, isFileEmpty);
    }
}
