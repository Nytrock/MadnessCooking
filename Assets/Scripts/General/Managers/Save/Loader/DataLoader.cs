using AYellowpaper;
using UnityEngine;

public abstract class DataLoader<TData> : MonoBehaviour
    where TData : ISaveable {

    [SerializeField] protected InterfaceReference<ILoadable<TData>>[] _loadables;

    public void Load(TData data, bool isFileEmpty) {
        foreach (var loadable in _loadables)
            loadable.Value.Load(data, isFileEmpty);
    }
}
