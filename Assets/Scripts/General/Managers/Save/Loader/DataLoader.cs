using UnityEngine;

public abstract class DataLoader<TData> : MonoBehaviour
    where TData : ISaveable {

    public abstract void Load(TData data, bool isFileEmpty);
}
