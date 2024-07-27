using AYellowpaper;
using UnityEngine;

public abstract class LocalDataLoader<TMainData, TData> : MonoBehaviour, ILoadable<TMainData>
    where TMainData : ISaveable where TData : ISaveable {

    [SerializeField] protected InterfaceReference<IBindable<TData>>[] _bindables;
    protected TData _data;

    public void Load(TMainData data, bool isFileEmpty) {
        SetData(data);
        foreach (var bindable in _bindables)
            bindable.Value.Bind(_data, isFileEmpty);
    }

    protected abstract void SetData(TMainData data);
}
