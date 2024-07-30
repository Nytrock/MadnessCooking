using AYellowpaper;
using UnityEngine;

public abstract class DataBinder<TData> : MonoBehaviour
    where TData : ISaveable {

    [SerializeField] private InterfaceReference<IBindable<TData>>[] _bindables;

    public virtual void Bind(TData data) {
        foreach (var bindable in _bindables)
            bindable.Value.Bind(data);
    }
}
