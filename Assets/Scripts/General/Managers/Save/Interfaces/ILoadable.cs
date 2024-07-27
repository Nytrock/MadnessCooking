public interface ILoadable<TData>
    where TData : ISaveable {

    void Load(TData data, bool isFileEmpty);
}
