public interface IBindable<TData>
    where TData : ISaveable {

    void Bind(TData data, bool isFileEmpty);
}
